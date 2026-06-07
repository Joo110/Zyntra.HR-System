using MediatR; using AutoMapper; using HRMS.Application.Common.Interfaces; using HRMS.Application.Modules.Positions.DTOs; using HRMS.Domain.Entities; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Positions.Queries.GetAllPositions;
public record GetAllPositionsQuery(int PageNumber = 1, int PageSize = 20) : IRequest<Result<PagedResult<PositionDto>>>;
public class GetAllPositionsQueryHandler : IRequestHandler<GetAllPositionsQuery, Result<PagedResult<PositionDto>>>
{
    private readonly IUnitOfWork _uow; private readonly IMapper _mapper;
    public GetAllPositionsQueryHandler(IUnitOfWork uow, IMapper mapper) { _uow = uow; _mapper = mapper; }
    public async Task<Result<PagedResult<PositionDto>>> Handle(GetAllPositionsQuery request, CancellationToken cancellationToken)
    {
        var query = _uow.Repository<Position>().GetQueryable().Where(p => !p.IsDeleted);
        var total = query.Count();
        var items = query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();
        return Result<PagedResult<PositionDto>>.Success(PagedResult<PositionDto>.Create(_mapper.Map<IEnumerable<PositionDto>>(items), total, request.PageNumber, request.PageSize));
    }
}
