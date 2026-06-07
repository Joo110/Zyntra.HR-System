using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HRMS.Application.Modules.Leave.Commands.ApproveLeaveRequest;
using HRMS.Application.Modules.Leave.Commands.CreateLeaveRequest;
using HRMS.Application.Modules.Leave.Commands.DeleteLeaveRequest;
using HRMS.Application.Modules.Leave.Queries.GetAllLeaves;
using HRMS.Application.Modules.Leave.Queries.GetLeaveById;
namespace HRMS.WebApi.Controllers.V1;
[ApiVersion("1.0")] [Authorize]
public class LeaveController : ApiBaseController
{
    [HttpGet] public async Task<IActionResult> GetAll([FromQuery] Guid? employeeId, [FromQuery] int pn = 1, [FromQuery] int ps = 20, CancellationToken ct = default) => HandleResult(await Mediator.Send(new GetAllLeavesQuery(employeeId, pn, ps), ct));
    [HttpGet("{id:guid}")] public async Task<IActionResult> GetById(Guid id, CancellationToken ct) => HandleResult(await Mediator.Send(new GetLeaveByIdQuery(id), ct));
    [HttpPost] public async Task<IActionResult> Create([FromBody] CreateLeaveRequestCommand cmd, CancellationToken ct) => HandleCreatedResult(await Mediator.Send(cmd, ct));
    [HttpPut("{id:guid}/approve")] public async Task<IActionResult> Approve(Guid id, [FromBody] ApproveLeaveRequestCommand cmd, CancellationToken ct) => HandleResult(await Mediator.Send(cmd, ct));
    [HttpDelete("{id:guid}")] public async Task<IActionResult> Delete(Guid id, CancellationToken ct) => HandleResult(await Mediator.Send(new DeleteLeaveRequestCommand(id), ct));
}
