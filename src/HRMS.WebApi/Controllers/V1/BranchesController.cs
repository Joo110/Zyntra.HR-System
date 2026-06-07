using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HRMS.Application.Modules.Branches.Commands.CreateBranch;
using HRMS.Application.Modules.Branches.Commands.DeleteBranch;
using HRMS.Application.Modules.Branches.Commands.UpdateBranch;
using HRMS.Application.Modules.Branches.Queries.GetAllBranches;
using HRMS.Application.Modules.Branches.Queries.GetBranchById;
namespace HRMS.WebApi.Controllers.V1;
[ApiVersion("1.0")] [Authorize]
public class BranchesController : ApiBaseController
{
    [HttpGet] public async Task<IActionResult> GetAll([FromQuery] int pn = 1, [FromQuery] int ps = 20, CancellationToken ct = default) => HandleResult(await Mediator.Send(new GetAllBranchesQuery(pn, ps), ct));
    [HttpGet("{id:guid}")] public async Task<IActionResult> GetById(Guid id, CancellationToken ct) => HandleResult(await Mediator.Send(new GetBranchByIdQuery(id), ct));
    [HttpPost] public async Task<IActionResult> Create([FromBody] CreateBranchCommand cmd, CancellationToken ct) => HandleCreatedResult(await Mediator.Send(cmd, ct));
    [HttpPut("{id:guid}")] public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBranchCommand cmd, CancellationToken ct) { if (id != cmd.Id) return BadRequest(); return HandleResult(await Mediator.Send(cmd, ct)); }
    [HttpDelete("{id:guid}")] public async Task<IActionResult> Delete(Guid id, CancellationToken ct) => HandleResult(await Mediator.Send(new DeleteBranchCommand(id), ct));
}
