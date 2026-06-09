using MediatR; using HRMS.Application.Common.Interfaces; using HRMS.Domain.Entities; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Leave.Commands.DeleteLeaveRequest;
public record DeleteLeaveRequestCommand(Guid Id) : IRequest<Result>;
public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand, Result>
{
    private readonly IUnitOfWork _uow;
    private ILocalizationService _localizer;
    public DeleteLeaveRequestCommandHandler(IUnitOfWork uow, ILocalizationService localizer)
    {
        _uow = uow;
        _localizer = localizer;
    }
    public async Task<Result> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leave = await _uow.Repository<LeaveRequest>().GetByIdAsync(request.Id, cancellationToken);
        if (leave is null) return Result.Failure(_localizer["Leaverequestnotfound"]);
        leave.IsDeleted = true; leave.DeletedAt = DateTime.UtcNow;
        _uow.Repository<LeaveRequest>().Update(leave);
        await _uow.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
