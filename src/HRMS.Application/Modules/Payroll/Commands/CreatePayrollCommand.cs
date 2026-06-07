using MediatR; using AutoMapper; using HRMS.Application.Common.Interfaces; using HRMS.Application.Modules.Payroll.DTOs; using HRMS.Domain.Entities; using HRMS.Shared.Helpers; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Payroll.Commands.CreatePayroll;
public record CreatePayrollCommand(int Month, int Year, string? Notes) : IRequest<Result<PayrollBatchDto>>;
public class CreatePayrollCommandHandler : IRequestHandler<CreatePayrollCommand, Result<PayrollBatchDto>>
{
    private readonly IUnitOfWork _uow; private readonly IMapper _mapper;
    public CreatePayrollCommandHandler(IUnitOfWork uow, IMapper mapper) { _uow = uow; _mapper = mapper; }
    public async Task<Result<PayrollBatchDto>> Handle(CreatePayrollCommand request, CancellationToken cancellationToken)
    {
        var exists = await _uow.Repository<PayrollBatch>().AnyAsync(p => p.Month == request.Month && p.Year == request.Year && !p.IsDeleted, cancellationToken);
        if (exists) return Result<PayrollBatchDto>.Failure($"Payroll batch for {request.Month}/{request.Year} already exists.");
        var batch = new PayrollBatch { BatchCode = CodeGenerator.GenerateCode("PAY"), Month = request.Month, Year = request.Year, ProcessedDate = DateTime.UtcNow, Notes = request.Notes };
        await _uow.Repository<PayrollBatch>().AddAsync(batch, cancellationToken);
        var employees = _uow.Repository<Domain.Entities.Employee>().GetQueryable().Where(e => !e.IsDeleted && e.Status == Domain.Enums.EmployeeStatus.Active).ToList();
        foreach (var emp in employees)
        {
            var payslip = new Payslip { EmployeeId = emp.Id, PayrollBatchId = batch.Id, Month = request.Month, Year = request.Year, BasicSalary = emp.BasicSalary ?? 0, TotalAllowances = 0, TotalDeductions = 0, TaxAmount = 0, NetSalary = emp.BasicSalary ?? 0 };
            await _uow.Repository<Payslip>().AddAsync(payslip, cancellationToken);
        }
        await _uow.SaveChangesAsync(cancellationToken);
        return Result<PayrollBatchDto>.Success(_mapper.Map<PayrollBatchDto>(batch));
    }
}
