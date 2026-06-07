using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HRMS.Application.Modules.Notifications.Commands.SendNotification;
using HRMS.Application.Modules.Notifications.Queries.GetAllNotifications;
namespace HRMS.WebApi.Controllers.V1;
[ApiVersion("1.0")] [Authorize]
public class NotificationsController : ApiBaseController
{
    [HttpGet] public async Task<IActionResult> GetAll([FromQuery] string? recipientId, [FromQuery] int pn = 1, [FromQuery] int ps = 20, CancellationToken ct = default) => HandleResult(await Mediator.Send(new GetAllNotificationsQuery(recipientId, pn, ps), ct));
    [HttpPost] public async Task<IActionResult> Send([FromBody] SendNotificationCommand cmd, CancellationToken ct) => HandleCreatedResult(await Mediator.Send(cmd, ct));
}
