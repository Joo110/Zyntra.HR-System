using MediatR; using AutoMapper; using HRMS.Application.Common.Interfaces; using HRMS.Application.Modules.Performance.DTOs; using HRMS.Domain.Entities; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Performance.Queries.GetAllKpis;
public record GetAllKpisQuery(int PageNumber = 1, int PageSize = 20) : IRequest<Result<PagedResult<KpiDto>>>;
public class GetAllKpisQueryHandler : IRequestHandler<GetAllKpisQuery, Result<PagedResult<KpiDto>>>
{
    private readonly IUnitOfWork _uow; private readonly IMapper _mapper;
    public GetAllKpisQueryHandler(IUnitOfWork uow, IMapper mapper) { _uow = uow; _mapper = mapper; }
    public async Task<Result<PagedResult<KpiDto>>> Handle(GetAllKpisQuery request, CancellationToken cancellationToken)
    {
        var query = _uow.Repository<KPI>().GetQueryable().Where(k => !k.IsDeleted);
        var total = query.Count();
        var items = query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();
        return Result<PagedResult<KpiDto>>.Success(PagedResult<KpiDto>.Create(_mapper.Map<IEnumerable<KpiDto>>(items), total, request.PageNumber, request.PageSize));
    }
}
