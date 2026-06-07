using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HRMS.Application.Modules.Users.Commands.CreateUser;
using HRMS.Application.Modules.Users.Commands.DeleteUser;
using HRMS.Application.Modules.Roles.Commands.CreateRole;
using HRMS.Application.Modules.Roles.Commands.DeleteRole;
using HRMS.Domain.Constants;
namespace HRMS.WebApi.Controllers.V1;
[ApiVersion("1.0")]
[Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.SuperAdmin)]
public class UsersController : ApiBaseController
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command, CancellationToken ct)
        => HandleResult(await Mediator.Send(command, ct));

    [HttpDelete("{userId}")]
    public async Task<IActionResult> Delete(string userId, CancellationToken ct)
        => HandleResult(await Mediator.Send(new DeleteUserCommand(userId), ct));

    [HttpPost("{userId}/roles")]
    public async Task<IActionResult> AssignRole(string userId, [FromBody] string role, CancellationToken ct)
        => HandleResult(await Mediator.Send(new AssignRoleCommand(userId, role), ct));

    [HttpDelete("{userId}/roles/{role}")]
    public async Task<IActionResult> RemoveRole(string userId, string role, CancellationToken ct)
        => HandleResult(await Mediator.Send(new RemoveRoleCommand(userId, role), ct));
}
