using MediatR; using HRMS.Application.Common.Interfaces; using HRMS.Domain.Entities; using HRMS.Domain.Enums; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Leave.Commands.ApproveLeaveRequest;
public record ApproveLeaveRequestCommand(Guid Id, bool IsApproved, string? RejectionReason, string ApprovedBy) : IRequest<Result>;
public class ApproveLeaveRequestCommandHandler : IRequestHandler<ApproveLeaveRequestCommand, Result>
{
    private readonly IUnitOfWork _uow;
    private ILocalizationService _localizer;
    public ApproveLeaveRequestCommandHandler(IUnitOfWork uow, ILocalizationService localizer)
    {
        _uow = uow;
        _localizer = localizer;
    }
    public async Task<Result> Handle(ApproveLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leave = await _uow.Repository<LeaveRequest>().GetByIdAsync(request.Id, cancellationToken);
        if (leave is null) return Result.Failure (_localizer["Leaverequestnotfound"]);
        leave.Status = request.IsApproved ? LeaveStatus.Approved : LeaveStatus.Rejected;
        leave.ApprovedBy = request.ApprovedBy; leave.ApprovedAt = DateTime.UtcNow; leave.RejectionReason = request.RejectionReason;
        _uow.Repository<LeaveRequest>().Update(leave);
        await _uow.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
