using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HRMS.Application.Modules.Employees.Commands.CreateEmployee;
using HRMS.Application.Modules.Employees.Commands.DeleteEmployee;
using HRMS.Application.Modules.Employees.Commands.UpdateEmployee;
using HRMS.Application.Modules.Employees.Queries.GetAllEmployees;
using HRMS.Application.Modules.Employees.Queries.GetEmployeeById;
using HRMS.Domain.Constants;
using HRMS.Domain.Enums;

namespace HRMS.WebApi.Controllers.V1;

[ApiVersion("1.0")]
[Authorize]
public class EmployeesController : ApiBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20, [FromQuery] string? searchTerm = null, CancellationToken ct = default)
        => HandleResult(await Mediator.Send(new GetAllEmployeesQuery(pageNumber, pageSize, searchTerm), ct));

    [HttpGet("{id:guid}", Name = "GetEmployeeById")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        => HandleResult(await Mediator.Send(new GetEmployeeByIdQuery(id), ct));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeCommand command, CancellationToken ct)
        => HandleCreatedResult(await Mediator.Send(command, ct));

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEmployeeCommand command, CancellationToken ct)
    {
        if (id != command.Id) return BadRequest("ID mismatch.");
        return HandleResult(await Mediator.Send(command, ct));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        => HandleResult(await Mediator.Send(new DeleteEmployeeCommand(id), ct));
}
