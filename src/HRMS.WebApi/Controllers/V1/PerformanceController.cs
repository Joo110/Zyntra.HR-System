using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HRMS.Application.Modules.Performance.Commands.CreateKpi;
using HRMS.Application.Modules.Performance.Commands.CreatePerformanceReview;
using HRMS.Application.Modules.Performance.Commands.DeleteKpi;
using HRMS.Application.Modules.Performance.Queries.GetAllKpis;
using HRMS.Application.Modules.Performance.Queries.GetAllPerformanceReviews;
using HRMS.Application.Modules.Performance.Queries.GetKpiById;
namespace HRMS.WebApi.Controllers.V1;
[ApiVersion("1.0")] [Authorize]
public class PerformanceController : ApiBaseController
{
    [HttpGet("kpis")] public async Task<IActionResult> GetKpis([FromQuery] int pn = 1, [FromQuery] int ps = 20, CancellationToken ct = default) => HandleResult(await Mediator.Send(new GetAllKpisQuery(pn, ps), ct));
    [HttpGet("kpis/{id:guid}")] public async Task<IActionResult> GetKpi(Guid id, CancellationToken ct) => HandleResult(await Mediator.Send(new GetKpiByIdQuery(id), ct));
    [HttpPost("kpis")] public async Task<IActionResult> CreateKpi([FromBody] CreateKpiCommand cmd, CancellationToken ct) => HandleCreatedResult(await Mediator.Send(cmd, ct));
    [HttpDelete("kpis/{id:guid}")] public async Task<IActionResult> DeleteKpi(Guid id, CancellationToken ct) => HandleResult(await Mediator.Send(new DeleteKpiCommand(id), ct));
    [HttpGet("reviews")] public async Task<IActionResult> GetReviews([FromQuery] Guid? employeeId, [FromQuery] int? year, [FromQuery] int pn = 1, [FromQuery] int ps = 20, CancellationToken ct = default) => HandleResult(await Mediator.Send(new GetAllPerformanceReviewsQuery(employeeId, year, pn, ps), ct));
    [HttpPost("reviews")] public async Task<IActionResult> CreateReview([FromBody] CreatePerformanceReviewCommand cmd, CancellationToken ct) => HandleCreatedResult(await Mediator.Send(cmd, ct));
}
