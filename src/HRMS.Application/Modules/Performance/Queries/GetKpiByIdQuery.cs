using MediatR; using AutoMapper; using HRMS.Application.Common.Interfaces; using HRMS.Application.Modules.Performance.DTOs; using HRMS.Domain.Entities; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Performance.Queries.GetKpiById;
public record GetKpiByIdQuery(Guid Id) : IRequest<Result<KpiDto>>;
public class GetKpiByIdQueryHandler : IRequestHandler<GetKpiByIdQuery, Result<KpiDto>>
{
    private readonly IUnitOfWork _uow; private readonly IMapper _mapper;
    public GetKpiByIdQueryHandler(IUnitOfWork uow, IMapper mapper) { _uow = uow; _mapper = mapper; }
    public async Task<Result<KpiDto>> Handle(GetKpiByIdQuery request, CancellationToken cancellationToken)
    {
        var kpi = await _uow.Repository<KPI>().GetByIdAsync(request.Id, cancellationToken);
        if (kpi is null || kpi.IsDeleted) return Result<KpiDto>.Failure("KPI not found.");
        return Result<KpiDto>.Success(_mapper.Map<KpiDto>(kpi));
    }
}
