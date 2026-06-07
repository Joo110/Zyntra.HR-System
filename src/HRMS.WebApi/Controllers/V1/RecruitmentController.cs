using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HRMS.Application.Modules.Recruitment.Commands.CreateCandidate;
using HRMS.Application.Modules.Recruitment.Commands.CreateJobVacancy;
using HRMS.Application.Modules.Recruitment.Commands.DeleteJobVacancy;
using HRMS.Application.Modules.Recruitment.Queries.GetAllCandidates;
using HRMS.Application.Modules.Recruitment.Queries.GetAllJobVacancies;
using HRMS.Application.Modules.Recruitment.Queries.GetJobVacancyById;
namespace HRMS.WebApi.Controllers.V1;
[ApiVersion("1.0")] [Authorize]
public class RecruitmentController : ApiBaseController
{
    [HttpGet("vacancies")] public async Task<IActionResult> GetVacancies([FromQuery] int pn = 1, [FromQuery] int ps = 20, CancellationToken ct = default) => HandleResult(await Mediator.Send(new GetAllJobVacanciesQuery(pn, ps), ct));
    [HttpGet("vacancies/{id:guid}")] public async Task<IActionResult> GetVacancy(Guid id, CancellationToken ct) => HandleResult(await Mediator.Send(new GetJobVacancyByIdQuery(id), ct));
    [HttpPost("vacancies")] public async Task<IActionResult> CreateVacancy([FromBody] CreateJobVacancyCommand cmd, CancellationToken ct) => HandleCreatedResult(await Mediator.Send(cmd, ct));
    [HttpDelete("vacancies/{id:guid}")] public async Task<IActionResult> DeleteVacancy(Guid id, CancellationToken ct) => HandleResult(await Mediator.Send(new DeleteJobVacancyCommand(id), ct));
    [HttpGet("candidates")] public async Task<IActionResult> GetCandidates([FromQuery] Guid? jobVacancyId, [FromQuery] int pn = 1, [FromQuery] int ps = 20, CancellationToken ct = default) => HandleResult(await Mediator.Send(new GetAllCandidatesQuery(jobVacancyId, pn, ps), ct));
    [HttpPost("candidates")] public async Task<IActionResult> CreateCandidate([FromBody] CreateCandidateCommand cmd, CancellationToken ct) => HandleCreatedResult(await Mediator.Send(cmd, ct));
}
