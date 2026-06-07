using MediatR; using AutoMapper; using HRMS.Application.Common.Interfaces; using HRMS.Application.Modules.Recruitment.DTOs; using HRMS.Domain.Entities; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Recruitment.Queries.GetAllJobVacancies;
public record GetAllJobVacanciesQuery(int PageNumber = 1, int PageSize = 20) : IRequest<Result<PagedResult<JobVacancyDto>>>;
public class GetAllJobVacanciesQueryHandler : IRequestHandler<GetAllJobVacanciesQuery, Result<PagedResult<JobVacancyDto>>>
{
    private readonly IUnitOfWork _uow; private readonly IMapper _mapper;
    public GetAllJobVacanciesQueryHandler(IUnitOfWork uow, IMapper mapper) { _uow = uow; _mapper = mapper; }
    public async Task<Result<PagedResult<JobVacancyDto>>> Handle(GetAllJobVacanciesQuery request, CancellationToken cancellationToken)
    {
        var query = _uow.Repository<JobVacancy>().GetQueryable().Where(j => !j.IsDeleted).OrderByDescending(j => j.PostedDate);
        var total = query.Count();
        var items = query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();
        return Result<PagedResult<JobVacancyDto>>.Success(PagedResult<JobVacancyDto>.Create(_mapper.Map<IEnumerable<JobVacancyDto>>(items), total, request.PageNumber, request.PageSize));
    }
}
