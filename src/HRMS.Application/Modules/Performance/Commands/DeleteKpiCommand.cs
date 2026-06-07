using MediatR; using HRMS.Application.Common.Interfaces; using HRMS.Domain.Entities; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Performance.Commands.DeleteKpi;
public record DeleteKpiCommand(Guid Id) : IRequest<Result>;
public class DeleteKpiCommandHandler : IRequestHandler<DeleteKpiCommand, Result>
{
    private readonly IUnitOfWork _uow;
    public DeleteKpiCommandHandler(IUnitOfWork uow) { _uow = uow; }
    public async Task<Result> Handle(DeleteKpiCommand request, CancellationToken cancellationToken)
    {
        var kpi = await _uow.Repository<KPI>().GetByIdAsync(request.Id, cancellationToken);
        if (kpi is null) return Result.Failure("KPI not found.");
        kpi.IsDeleted = true; kpi.DeletedAt = DateTime.UtcNow;
        _uow.Repository<KPI>().Update(kpi);
        await _uow.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
