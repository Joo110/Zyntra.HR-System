using MediatR; using AutoMapper; using HRMS.Application.Common.Interfaces; using HRMS.Application.Modules.Performance.DTOs; using HRMS.Domain.Entities; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Performance.Queries.GetAllPerformanceReviews;
public record GetAllPerformanceReviewsQuery(Guid? EmployeeId, int? Year, int PageNumber = 1, int PageSize = 20) : IRequest<Result<PagedResult<PerformanceReviewDto>>>;
public class GetAllPerformanceReviewsQueryHandler : IRequestHandler<GetAllPerformanceReviewsQuery, Result<PagedResult<PerformanceReviewDto>>>
{
    private readonly IUnitOfWork _uow; private readonly IMapper _mapper;
    public GetAllPerformanceReviewsQueryHandler(IUnitOfWork uow, IMapper mapper) { _uow = uow; _mapper = mapper; }
    public async Task<Result<PagedResult<PerformanceReviewDto>>> Handle(GetAllPerformanceReviewsQuery request, CancellationToken cancellationToken)
    {
        var query = _uow.Repository<PerformanceReview>().GetQueryable().Where(p => !p.IsDeleted);
        if (request.EmployeeId.HasValue) query = query.Where(p => p.EmployeeId == request.EmployeeId.Value);
        if (request.Year.HasValue) query = query.Where(p => p.ReviewYear == request.Year.Value);
        var total = query.Count();
        var items = query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();
        return Result<PagedResult<PerformanceReviewDto>>.Success(PagedResult<PerformanceReviewDto>.Create(_mapper.Map<IEnumerable<PerformanceReviewDto>>(items), total, request.PageNumber, request.PageSize));
    }
}
