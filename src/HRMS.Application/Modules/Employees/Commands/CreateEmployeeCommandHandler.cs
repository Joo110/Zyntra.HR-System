using AutoMapper;
using MediatR;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.Modules.Employees.DTOs;
using HRMS.Domain.Entities;
using HRMS.Shared.Helpers;
using HRMS.Shared.Models;

namespace HRMS.Application.Modules.Employees.Commands.CreateEmployee;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Result<EmployeeDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly IAuditService _auditService;
    private readonly ILocalizationService _localizer;

    public CreateEmployeeCommandHandler(
        IUnitOfWork uow,
        IMapper mapper,
        IAuditService auditService,
        ILocalizationService localizer)
    {
        _uow = uow;
        _mapper = mapper;
        _auditService = auditService;
        _localizer = localizer;
    }

    public async Task<Result<EmployeeDto>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employeeRepo = _uow.Repository<Employee>();

        var existing = await employeeRepo.AnyAsync(
            e => e.Email == request.Email && !e.IsDeleted,
            cancellationToken);

        if (existing)
        {
            return Result<EmployeeDto>.Failure(
                _localizer["EmployeeEmailExists", request.Email]);
        }

        var employee = new Employee
        {
            EmployeeCode = CodeGenerator.GenerateCode("EMP"),
            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            Email = request.Email,
            Phone = request.Phone,
            Mobile = request.Mobile,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender,
            MaritalStatus = request.MaritalStatus,
            NationalId = request.NationalId,
            Nationality = request.Nationality,
            Address = request.Address,
            City = request.City,
            Country = request.Country,
            HireDate = request.HireDate,
            ContractType = request.ContractType,
            DepartmentId = request.DepartmentId,
            PositionId = request.PositionId,
            BranchId = request.BranchId,
            ManagerId = request.ManagerId,
            BasicSalary = request.BasicSalary
        };

        await employeeRepo.AddAsync(employee, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        await _auditService.LogAsync(
            HRMS.Domain.Enums.AuditAction.Create,
            nameof(Employee),
            employee.Id.ToString(),
            cancellationToken: cancellationToken);

        var dto = _mapper.Map<EmployeeDto>(employee);
        return Result<EmployeeDto>.Success(dto);
    }
}