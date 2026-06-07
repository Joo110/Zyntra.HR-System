using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HRMS.Application.Modules.Departments.Commands.CreateDepartment;
using HRMS.Application.Modules.Departments.Commands.DeleteDepartment;
using HRMS.Application.Modules.Departments.Commands.UpdateDepartment;
using HRMS.Application.Modules.Departments.Queries.GetAllDepartments;
using HRMS.Application.Modules.Departments.Queries.GetDepartmentById;

namespace HRMS.WebApi.Controllers.V1;

[ApiVersion("1.0")]
[Authorize]
public class DepartmentsController : ApiBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20, [FromQuery] string? searchTerm = null, CancellationToken ct = default)
        => HandleResult(await Mediator.Send(new GetAllDepartmentsQuery(pageNumber, pageSize, searchTerm), ct));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        => HandleResult(await Mediator.Send(new GetDepartmentByIdQuery(id), ct));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDepartmentCommand command, CancellationToken ct)
        => HandleCreatedResult(await Mediator.Send(command, ct));

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateDepartmentCommand command, CancellationToken ct)
    {
        if (id != command.Id) return BadRequest("ID mismatch.");
        return HandleResult(await Mediator.Send(command, ct));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        => HandleResult(await Mediator.Send(new DeleteDepartmentCommand(id), ct));
}
