using MediatR; using HRMS.Application.Common.Interfaces; using HRMS.Domain.Entities; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Payroll.Commands.DeletePayroll;
public record DeletePayrollCommand(Guid Id) : IRequest<Result>;
public class DeletePayrollCommandHandler : IRequestHandler<DeletePayrollCommand, Result>
{
    private readonly IUnitOfWork _uow;
    public DeletePayrollCommandHandler(IUnitOfWork uow) { _uow = uow; }
    public async Task<Result> Handle(DeletePayrollCommand request, CancellationToken cancellationToken)
    {
        var batch = await _uow.Repository<PayrollBatch>().GetByIdAsync(request.Id, cancellationToken);
        if (batch is null) return Result.Failure("Payroll batch not found.");
        if (batch.Status != Domain.Enums.PayrollStatus.Draft) return Result.Failure("Only draft payroll batches can be deleted.");
        batch.IsDeleted = true; batch.DeletedAt = DateTime.UtcNow;
        _uow.Repository<PayrollBatch>().Update(batch);
        await _uow.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
