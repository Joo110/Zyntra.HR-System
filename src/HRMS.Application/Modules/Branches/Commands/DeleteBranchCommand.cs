using MediatR; using HRMS.Application.Common.Interfaces; using HRMS.Domain.Entities; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Branches.Commands.DeleteBranch;
public record DeleteBranchCommand(Guid Id) : IRequest<Result>;
public class DeleteBranchCommandHandler : IRequestHandler<DeleteBranchCommand, Result>
{
    private readonly IUnitOfWork _uow;
    public DeleteBranchCommandHandler(IUnitOfWork uow) { _uow = uow; }
    public async Task<Result> Handle(DeleteBranchCommand request, CancellationToken cancellationToken)
    {
        var b = await _uow.Repository<Branch>().GetByIdAsync(request.Id, cancellationToken);
        if (b is null) return Result.Failure("Branch not found.");
        b.IsDeleted = true; b.DeletedAt = DateTime.UtcNow;
        _uow.Repository<Branch>().Update(b);
        await _uow.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
