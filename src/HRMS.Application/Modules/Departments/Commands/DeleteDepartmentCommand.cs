using MediatR; using HRMS.Application.Common.Interfaces; using HRMS.Domain.Entities; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Departments.Commands.DeleteDepartment;
public record DeleteDepartmentCommand(Guid Id) : IRequest<Result>;
public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, Result>
{
    private readonly IUnitOfWork _uow;
    public DeleteDepartmentCommandHandler(IUnitOfWork uow) { _uow = uow; }
    public async Task<Result> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
    {
        var dept = await _uow.Repository<Department>().GetByIdAsync(request.Id, cancellationToken);
        if (dept is null) return Result.Failure($"Department '{request.Id}' not found.");
        dept.IsDeleted = true; dept.DeletedAt = DateTime.UtcNow;
        _uow.Repository<Department>().Update(dept);
        await _uow.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
