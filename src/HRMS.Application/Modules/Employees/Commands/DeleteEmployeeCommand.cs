using MediatR;
using HRMS.Application.Common.Interfaces;
using HRMS.Domain.Entities;
using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Employees.Commands.DeleteEmployee;
public record DeleteEmployeeCommand(Guid Id) : IRequest<Result>;
public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Result>
{
    private readonly IUnitOfWork _uow;
    private readonly IAuditService _auditService;
    private ILocalizationService _localizer;
    public DeleteEmployeeCommandHandler(IUnitOfWork uow, IAuditService auditService, ILocalizationService localizer)
    {
        _uow = uow; _auditService = auditService;
        _localizer = localizer;
    }
    public async Task<Result> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _uow.Repository<Employee>().GetByIdAsync(request.Id, cancellationToken);
        if (employee is null)
            return Result.Failure(
                _localizer["EmployeeNotFound", request.Id]);
        employee.IsDeleted = true; employee.DeletedAt = DateTime.UtcNow;
        _uow.Repository<Employee>().Update(employee);
        await _uow.SaveChangesAsync(cancellationToken);
        await _auditService.LogAsync(HRMS.Domain.Enums.AuditAction.Delete, nameof(Employee), employee.Id.ToString(), cancellationToken: cancellationToken);
        return Result.Success();
    }
}
