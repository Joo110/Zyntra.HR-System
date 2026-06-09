using MediatR; using HRMS.Application.Common.Interfaces; using HRMS.Domain.Entities; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Recruitment.Commands.DeleteJobVacancy;
public record DeleteJobVacancyCommand(Guid Id) : IRequest<Result>;
public class DeleteJobVacancyCommandHandler : IRequestHandler<DeleteJobVacancyCommand, Result>
{
    private readonly IUnitOfWork _uow;
    private ILocalizationService _localizer;
    public DeleteJobVacancyCommandHandler(IUnitOfWork uow, ILocalizationService localizer)
    {
        _uow = uow;
        _localizer = localizer;
    }
    public async Task<Result> Handle(DeleteJobVacancyCommand request, CancellationToken cancellationToken)
    {
        var job = await _uow.Repository<JobVacancy>().GetByIdAsync(request.Id, cancellationToken);
        if (job is null) return Result.Failure(_localizer["JobVacancyNotFound"]);
        job.IsDeleted = true; job.DeletedAt = DateTime.UtcNow;
        _uow.Repository<JobVacancy>().Update(job);
        await _uow.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
