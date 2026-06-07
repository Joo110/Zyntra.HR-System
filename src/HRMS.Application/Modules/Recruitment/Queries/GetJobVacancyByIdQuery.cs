using MediatR; using AutoMapper; using HRMS.Application.Common.Interfaces; using HRMS.Application.Modules.Recruitment.DTOs; using HRMS.Domain.Entities; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Recruitment.Queries.GetJobVacancyById;
public record GetJobVacancyByIdQuery(Guid Id) : IRequest<Result<JobVacancyDto>>;
public class GetJobVacancyByIdQueryHandler : IRequestHandler<GetJobVacancyByIdQuery, Result<JobVacancyDto>>
{
    private readonly IUnitOfWork _uow; private readonly IMapper _mapper;
    public GetJobVacancyByIdQueryHandler(IUnitOfWork uow, IMapper mapper) { _uow = uow; _mapper = mapper; }
    public async Task<Result<JobVacancyDto>> Handle(GetJobVacancyByIdQuery request, CancellationToken cancellationToken)
    {
        var j = await _uow.Repository<JobVacancy>().GetByIdAsync(request.Id, cancellationToken);
        if (j is null || j.IsDeleted) return Result<JobVacancyDto>.Failure("Job vacancy not found.");
        return Result<JobVacancyDto>.Success(_mapper.Map<JobVacancyDto>(j));
    }
}
