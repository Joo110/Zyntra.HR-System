using MediatR; using AutoMapper; using HRMS.Application.Common.Interfaces; using HRMS.Application.Modules.Recruitment.DTOs; using HRMS.Domain.Entities; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Recruitment.Queries.GetAllCandidates;
public record GetAllCandidatesQuery(Guid? JobVacancyId, int PageNumber = 1, int PageSize = 20) : IRequest<Result<PagedResult<CandidateDto>>>;
public class GetAllCandidatesQueryHandler : IRequestHandler<GetAllCandidatesQuery, Result<PagedResult<CandidateDto>>>
{
    private readonly IUnitOfWork _uow; private readonly IMapper _mapper;
    public GetAllCandidatesQueryHandler(IUnitOfWork uow, IMapper mapper) { _uow = uow; _mapper = mapper; }
    public async Task<Result<PagedResult<CandidateDto>>> Handle(GetAllCandidatesQuery request, CancellationToken cancellationToken)
    {
        var query = _uow.Repository<Candidate>().GetQueryable().Where(c => !c.IsDeleted);
        if (request.JobVacancyId.HasValue) query = query.Where(c => c.JobVacancyId == request.JobVacancyId.Value);
        var total = query.Count();
        var items = query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();
        return Result<PagedResult<CandidateDto>>.Success(PagedResult<CandidateDto>.Create(_mapper.Map<IEnumerable<CandidateDto>>(items), total, request.PageNumber, request.PageSize));
    }
}
