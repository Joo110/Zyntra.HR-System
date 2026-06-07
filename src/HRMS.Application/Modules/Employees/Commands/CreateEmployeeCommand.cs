using MediatR;
using HRMS.Application.Modules.Employees.DTOs;
using HRMS.Domain.Enums;
using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Employees.Commands.CreateEmployee;
public record CreateEmployeeCommand(
    string FirstName, string LastName, string? MiddleName, string Email,
    string? Phone, string? Mobile, DateTime DateOfBirth, Gender Gender,
    MaritalStatus MaritalStatus, string? NationalId, string? Nationality,
    string? Address, string? City, string? Country, DateTime HireDate,
    ContractType ContractType, Guid? DepartmentId, Guid? PositionId,
    Guid? BranchId, Guid? ManagerId, decimal? BasicSalary
) : IRequest<Result<EmployeeDto>>;
