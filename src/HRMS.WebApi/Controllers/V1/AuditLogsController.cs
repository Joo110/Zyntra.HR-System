using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HRMS.Application.Modules.AuditLogs.Queries.GetAllAuditLogs;
using HRMS.Domain.Constants;
namespace HRMS.WebApi.Controllers.V1;
[ApiVersion("1.0")] [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.SuperAdmin)]
public class AuditLogsController : ApiBaseController
{
    [HttpGet] public async Task<IActionResult> GetAll([FromQuery] string? userId, [FromQuery] string? entityName, [FromQuery] int pn = 1, [FromQuery] int ps = 20, CancellationToken ct = default) => HandleResult(await Mediator.Send(new GetAllAuditLogsQuery(userId, entityName, pn, ps), ct));
}
