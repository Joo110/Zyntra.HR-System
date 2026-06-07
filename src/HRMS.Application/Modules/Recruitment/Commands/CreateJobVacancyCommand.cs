using MediatR; using AutoMapper; using HRMS.Application.Common.Interfaces; using HRMS.Application.Modules.Recruitment.DTOs; using HRMS.Domain.Entities; using HRMS.Shared.Helpers; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Recruitment.Commands.CreateJobVacancy;
public record CreateJobVacancyCommand(string Title, string? Description, Guid? DepartmentId, int NumberOfPositions, DateTime PostedDate, DateTime? ClosingDate, decimal? MinSalary, decimal? MaxSalary, string? Requirements) : IRequest<Result<JobVacancyDto>>;
public class CreateJobVacancyCommandHandler : IRequestHandler<CreateJobVacancyCommand, Result<JobVacancyDto>>
{
    private readonly IUnitOfWork _uow; private readonly IMapper _mapper;
    public CreateJobVacancyCommandHandler(IUnitOfWork uow, IMapper mapper) { _uow = uow; _mapper = mapper; }
    public async Task<Result<JobVacancyDto>> Handle(CreateJobVacancyCommand request, CancellationToken cancellationToken)
    {
        var job = new JobVacancy { Code = CodeGenerator.GenerateCode("JOB"), Title = request.Title, Description = request.Description, DepartmentId = request.DepartmentId, NumberOfPositions = request.NumberOfPositions, PostedDate = request.PostedDate, ClosingDate = request.ClosingDate, MinSalary = request.MinSalary, MaxSalary = request.MaxSalary, Requirements = request.Requirements };
        await _uow.Repository<JobVacancy>().AddAsync(job, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
        return Result<JobVacancyDto>.Success(_mapper.Map<JobVacancyDto>(job));
    }
}
