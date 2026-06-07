using MediatR; using AutoMapper; using HRMS.Application.Common.Interfaces; using HRMS.Application.Modules.Departments.DTOs; using HRMS.Domain.Entities; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Departments.Queries.GetAllDepartments;
public record GetAllDepartmentsQuery(int PageNumber = 1, int PageSize = 20, string? SearchTerm = null) : IRequest<Result<PagedResult<DepartmentDto>>>;
public class GetAllDepartmentsQueryHandler : IRequestHandler<GetAllDepartmentsQuery, Result<PagedResult<DepartmentDto>>>
{
    private readonly IUnitOfWork _uow; private readonly IMapper _mapper;
    public GetAllDepartmentsQueryHandler(IUnitOfWork uow, IMapper mapper) { _uow = uow; _mapper = mapper; }
    public async Task<Result<PagedResult<DepartmentDto>>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
    {
        var query = _uow.Repository<Department>().GetQueryable().Where(d => !d.IsDeleted);
        if (!string.IsNullOrWhiteSpace(request.SearchTerm)) { var t = request.SearchTerm.ToLower(); query = query.Where(d => d.Name.ToLower().Contains(t) || d.Code.ToLower().Contains(t)); }
        var total = query.Count();
        var items = query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();
        return Result<PagedResult<DepartmentDto>>.Success(PagedResult<DepartmentDto>.Create(_mapper.Map<IEnumerable<DepartmentDto>>(items), total, request.PageNumber, request.PageSize));
    }
}
