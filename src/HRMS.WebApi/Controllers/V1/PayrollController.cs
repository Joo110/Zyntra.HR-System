using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HRMS.Application.Modules.Payroll.Commands.CreatePayroll;
using HRMS.Application.Modules.Payroll.Commands.DeletePayroll;
using HRMS.Application.Modules.Payroll.Queries.GetAllPayrolls;
using HRMS.Application.Modules.Payroll.Queries.GetPayrollById;
using HRMS.Application.Modules.Payroll.Queries.GetPayslip;
namespace HRMS.WebApi.Controllers.V1;
[ApiVersion("1.0")] [Authorize]
public class PayrollController : ApiBaseController
{
    [HttpGet] public async Task<IActionResult> GetAll([FromQuery] int pn = 1, [FromQuery] int ps = 20, CancellationToken ct = default) => HandleResult(await Mediator.Send(new GetAllPayrollsQuery(pn, ps), ct));
    [HttpGet("{id:guid}")] public async Task<IActionResult> GetById(Guid id, CancellationToken ct) => HandleResult(await Mediator.Send(new GetPayrollByIdQuery(id), ct));
    [HttpGet("payslip")] public async Task<IActionResult> GetPayslip([FromQuery] Guid employeeId, [FromQuery] int month, [FromQuery] int year, CancellationToken ct) => HandleResult(await Mediator.Send(new GetPayslipQuery(employeeId, month, year), ct));
    [HttpPost] public async Task<IActionResult> Create([FromBody] CreatePayrollCommand cmd, CancellationToken ct) => HandleCreatedResult(await Mediator.Send(cmd, ct));
    [HttpDelete("{id:guid}")] public async Task<IActionResult> Delete(Guid id, CancellationToken ct) => HandleResult(await Mediator.Send(new DeletePayrollCommand(id), ct));
}
