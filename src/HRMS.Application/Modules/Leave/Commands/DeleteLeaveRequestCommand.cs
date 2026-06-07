using MediatR; using HRMS.Application.Common.Interfaces; using HRMS.Domain.Entities; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Leave.Commands.DeleteLeaveRequest;
public record DeleteLeaveRequestCommand(Guid Id) : IRequest<Result>;
public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand, Result>
{
    private readonly IUnitOfWork _uow;
    public DeleteLeaveRequestCommandHandler(IUnitOfWork uow) { _uow = uow; }
    public async Task<Result> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leave = await _uow.Repository<LeaveRequest>().GetByIdAsync(request.Id, cancellationToken);
        if (leave is null) return Result.Failure("Leave request not found.");
        leave.IsDeleted = true; leave.DeletedAt = DateTime.UtcNow;
        _uow.Repository<LeaveRequest>().Update(leave);
        await _uow.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
