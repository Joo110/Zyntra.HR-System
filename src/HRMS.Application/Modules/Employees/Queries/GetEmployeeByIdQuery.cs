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
    private ILocalizationService _localizer;
    public GetEmployeeByIdQueryHandler(IUnitOfWork uow, IMapper mapper, ILocalizationService localizer)
    {
        _uow = uow; _mapper = mapper;
        _localizer = localizer;
    }
    public async Task<Result<EmployeeDto>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var employee = await _uow.Repository<Employee>().GetByIdAsync(request.Id, cancellationToken);
        if (employee is null) return Result<EmployeeDto>.Failure(_localizer["EmployeeNotFound", request.Id]);
        return Result<EmployeeDto>.Success(_mapper.Map<EmployeeDto>(employee));
    }
}
