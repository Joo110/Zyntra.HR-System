using MediatR; using AutoMapper; using HRMS.Application.Common.Interfaces; using HRMS.Application.Modules.Positions.DTOs; using HRMS.Domain.Entities; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Positions.Queries.GetPositionById;
public record GetPositionByIdQuery(Guid Id) : IRequest<Result<PositionDto>>;
public class GetPositionByIdQueryHandler : IRequestHandler<GetPositionByIdQuery, Result<PositionDto>>
{
    private readonly IUnitOfWork _uow; private readonly IMapper _mapper;
    private ILocalizationService _localizer;
    public GetPositionByIdQueryHandler(IUnitOfWork uow, IMapper mapper, ILocalizationService localizer)
    {
        _uow = uow; _mapper = mapper;
        _localizer = localizer;
    }
    public async Task<Result<PositionDto>> Handle(GetPositionByIdQuery request, CancellationToken cancellationToken)
    {
        var pos = await _uow.Repository<Position>().GetByIdAsync(request.Id, cancellationToken);
        if (pos is null || pos.IsDeleted) return Result<PositionDto>.Failure(_localizer["PositionNotFound"]);
        return Result<PositionDto>.Success(_mapper.Map<PositionDto>(pos));
    }
}
