using MediatR;
using AutoMapper;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.Modules.Employees.DTOs;
using HRMS.Domain.Entities;
using HRMS.Domain.Enums;
using HRMS.Shared.Models;

namespace HRMS.Application.Modules.Employees.Commands.UpdateEmployee;

public record UpdateEmployeeCommand(
    Guid Id, string FirstName, string LastName, string? MiddleName, string Email,
    string? Phone, string? Mobile, DateTime DateOfBirth, Gender Gender, MaritalStatus MaritalStatus,
    string? Address, string? City, string? Country, EmployeeStatus Status,
    Guid? DepartmentId, Guid? PositionId, Guid? BranchId, Guid? ManagerId, decimal? BasicSalary
) : IRequest<Result<EmployeeDto>>;

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Result<EmployeeDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly IAuditService _auditService;

    public UpdateEmployeeCommandHandler(IUnitOfWork uow, IMapper mapper, IAuditService auditService)
    { _uow = uow; _mapper = mapper; _auditService = auditService; }

    public async Task<Result<EmployeeDto>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _uow.Repository<Employee>().GetByIdAsync(request.Id, cancellationToken);
        if (employee is null) return Result<EmployeeDto>.Failure($"Employee '{request.Id}' not found.");

        employee.FirstName = request.FirstName;
        employee.LastName = request.LastName;
        employee.MiddleName = request.MiddleName;
        employee.Email = request.Email;
        employee.Phone = request.Phone;
        employee.Mobile = request.Mobile;
        employee.DateOfBirth = request.DateOfBirth;
        employee.Gender = request.Gender;
        employee.MaritalStatus = request.MaritalStatus;
        employee.Address = request.Address;
        employee.City = request.City;
        employee.Country = request.Country;
        employee.Status = request.Status;
        employee.DepartmentId = request.DepartmentId;
        employee.PositionId = request.PositionId;
        employee.BranchId = request.BranchId;
        employee.ManagerId = request.ManagerId;
        employee.BasicSalary = request.BasicSalary;
        employee.UpdatedAt = DateTime.UtcNow;

        _uow.Repository<Employee>().Update(employee);
        await _uow.SaveChangesAsync(cancellationToken);
        await _auditService.LogAsync(Domain.Enums.AuditAction.Update, nameof(Employee), employee.Id.ToString(), cancellationToken: cancellationToken);
        return Result<EmployeeDto>.Success(_mapper.Map<EmployeeDto>(employee));
    }
}
