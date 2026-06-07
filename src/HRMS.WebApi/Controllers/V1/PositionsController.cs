using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HRMS.Application.Modules.Positions.Commands.CreatePosition;
using HRMS.Application.Modules.Positions.Commands.DeletePosition;
using HRMS.Application.Modules.Positions.Commands.UpdatePosition;
using HRMS.Application.Modules.Positions.Queries.GetAllPositions;
using HRMS.Application.Modules.Positions.Queries.GetPositionById;

namespace HRMS.WebApi.Controllers.V1;

[ApiVersion("1.0")]
[Authorize]
public class PositionsController : ApiBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20, CancellationToken ct = default)
        => HandleResult(await Mediator.Send(new GetAllPositionsQuery(pageNumber, pageSize), ct));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        => HandleResult(await Mediator.Send(new GetPositionByIdQuery(id), ct));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePositionCommand command, CancellationToken ct)
        => HandleCreatedResult(await Mediator.Send(command, ct));

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePositionCommand command, CancellationToken ct)
    {
        if (id != command.Id) return BadRequest("ID mismatch.");
        return HandleResult(await Mediator.Send(command, ct));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        => HandleResult(await Mediator.Send(new DeletePositionCommand(id), ct));
}
