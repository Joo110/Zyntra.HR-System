using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HRMS.Application.Modules.Permissions.Queries.GetAllPermissions;
using HRMS.Domain.Constants;
namespace HRMS.WebApi.Controllers.V1;
[ApiVersion("1.0")] [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.SuperAdmin)]
public class PermissionsController : ApiBaseController
{
    [HttpGet] public async Task<IActionResult> GetAll(CancellationToken ct) => HandleResult(await Mediator.Send(new GetAllPermissionsQuery(), ct));
}
