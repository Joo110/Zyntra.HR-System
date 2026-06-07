using MediatR;
using AutoMapper;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.Modules.Employees.DTOs;
using HRMS.Domain.Entities;
using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Employees.Queries.GetEmployeeById;
public record GetEmployeeByIdQuery(Guid Id) : IRequest<Result<EmployeeDto>>;
public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, Result<EmployeeDto>>
{
    private readonly IUnitOfWork _uow; private readonly IMapper _mapper;
    public GetEmployeeByIdQueryHandler(IUnitOfWork uow, IMapper mapper) { _uow = uow; _mapper = mapper; }
    public async Task<Result<EmployeeDto>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var employee = await _uow.Repository<Employee>().GetByIdAsync(request.Id, cancellationToken);
        if (employee is null || employee.IsDeleted) return Result<EmployeeDto>.Failure($"Employee '{request.Id}' not found.");
        return Result<EmployeeDto>.Success(_mapper.Map<EmployeeDto>(employee));
    }
}
