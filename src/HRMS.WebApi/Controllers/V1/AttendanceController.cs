using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HRMS.Application.Modules.Attendance.Commands.CheckIn;
using HRMS.Application.Modules.Attendance.Commands.CheckOut;
using HRMS.Application.Modules.Attendance.Queries.GetAllAttendance;
using HRMS.Application.Modules.Attendance.Queries.GetAttendanceById;
namespace HRMS.WebApi.Controllers.V1;
[ApiVersion("1.0")] [Authorize]
public class AttendanceController : ApiBaseController
{
    [HttpGet] public async Task<IActionResult> GetAll([FromQuery] Guid? employeeId, [FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] int pn = 1, [FromQuery] int ps = 20, CancellationToken ct = default) => HandleResult(await Mediator.Send(new GetAllAttendanceQuery(employeeId, from, to, pn, ps), ct));
    [HttpGet("{id:guid}")] public async Task<IActionResult> GetById(Guid id, CancellationToken ct) => HandleResult(await Mediator.Send(new GetAttendanceByIdQuery(id), ct));
    [HttpPost("check-in")] public async Task<IActionResult> CheckIn([FromBody] CheckInCommand cmd, CancellationToken ct) => HandleCreatedResult(await Mediator.Send(cmd, ct));
    [HttpPost("check-out")] public async Task<IActionResult> CheckOut([FromBody] CheckOutCommand cmd, CancellationToken ct) => HandleResult(await Mediator.Send(cmd, ct));
}
