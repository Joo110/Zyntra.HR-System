using MediatR;
using AutoMapper;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.Modules.Employees.DTOs;
using HRMS.Domain.Entities;
using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Employees.Queries.GetAllEmployees;
public record GetAllEmployeesQuery(int PageNumber = 1, int PageSize = 20, string? SearchTerm = null) : IRequest<Result<PagedResult<EmployeeDto>>>;
public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, Result<PagedResult<EmployeeDto>>>
{
    private readonly IUnitOfWork _uow; private readonly IMapper _mapper;
    public GetAllEmployeesQueryHandler(IUnitOfWork uow, IMapper mapper) { _uow = uow; _mapper = mapper; }
    public async Task<Result<PagedResult<EmployeeDto>>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
    {
        var query = _uow.Repository<Employee>().GetQueryable().Where(e => !e.IsDeleted);
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var term = request.SearchTerm.ToLower();
            query = query.Where(e => e.FirstName.ToLower().Contains(term) || e.LastName.ToLower().Contains(term) || e.Email.ToLower().Contains(term) || e.EmployeeCode.ToLower().Contains(term));
        }
        var total = query.Count();
        var items = query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();
        var dtos = _mapper.Map<IEnumerable<EmployeeDto>>(items);
        return Result<PagedResult<EmployeeDto>>.Success(PagedResult<EmployeeDto>.Create(dtos, total, request.PageNumber, request.PageSize));
    }
}
