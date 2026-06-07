using MediatR; using AutoMapper; using HRMS.Application.Common.Interfaces; using HRMS.Application.Modules.Recruitment.DTOs; using HRMS.Domain.Entities; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Recruitment.Commands.CreateCandidate;
public record CreateCandidateCommand(Guid JobVacancyId, string FirstName, string LastName, string Email, string? Phone, string? CoverLetter) : IRequest<Result<CandidateDto>>;
public class CreateCandidateCommandHandler : IRequestHandler<CreateCandidateCommand, Result<CandidateDto>>
{
    private readonly IUnitOfWork _uow; private readonly IMapper _mapper;
    public CreateCandidateCommandHandler(IUnitOfWork uow, IMapper mapper) { _uow = uow; _mapper = mapper; }
    public async Task<Result<CandidateDto>> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
    {
        var candidate = new Candidate { JobVacancyId = request.JobVacancyId, FirstName = request.FirstName, LastName = request.LastName, Email = request.Email, Phone = request.Phone, CoverLetter = request.CoverLetter };
        await _uow.Repository<Candidate>().AddAsync(candidate, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
        return Result<CandidateDto>.Success(_mapper.Map<CandidateDto>(candidate));
    }
}
