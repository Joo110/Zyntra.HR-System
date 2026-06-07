using MediatR; using HRMS.Application.Common.Interfaces; using HRMS.Domain.Entities; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Positions.Commands.DeletePosition;
public record DeletePositionCommand(Guid Id) : IRequest<Result>;
public class DeletePositionCommandHandler : IRequestHandler<DeletePositionCommand, Result>
{
    private readonly IUnitOfWork _uow;
    public DeletePositionCommandHandler(IUnitOfWork uow) { _uow = uow; }
    public async Task<Result> Handle(DeletePositionCommand request, CancellationToken cancellationToken)
    {
        var pos = await _uow.Repository<Position>().GetByIdAsync(request.Id, cancellationToken);
        if (pos is null) return Result.Failure("Position not found.");
        pos.IsDeleted = true; pos.DeletedAt = DateTime.UtcNow;
        _uow.Repository<Position>().Update(pos);
        await _uow.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
