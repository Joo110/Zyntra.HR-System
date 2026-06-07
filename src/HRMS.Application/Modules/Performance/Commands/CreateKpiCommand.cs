using MediatR; using AutoMapper; using HRMS.Application.Common.Interfaces; using HRMS.Application.Modules.Performance.DTOs; using HRMS.Domain.Entities; using HRMS.Shared.Helpers; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Performance.Commands.CreateKpi;
public record CreateKpiCommand(string Name, string? Description, Guid? DepartmentId, decimal TargetValue, string? Unit, int Weight) : IRequest<Result<KpiDto>>;
public class CreateKpiCommandHandler : IRequestHandler<CreateKpiCommand, Result<KpiDto>>
{
    private readonly IUnitOfWork _uow; private readonly IMapper _mapper;
    public CreateKpiCommandHandler(IUnitOfWork uow, IMapper mapper) { _uow = uow; _mapper = mapper; }
    public async Task<Result<KpiDto>> Handle(CreateKpiCommand request, CancellationToken cancellationToken)
    {
        var kpi = new KPI { Code = CodeGenerator.GenerateCode("KPI"), Name = request.Name, Description = request.Description, DepartmentId = request.DepartmentId, TargetValue = request.TargetValue, Unit = request.Unit, Weight = request.Weight };
        await _uow.Repository<KPI>().AddAsync(kpi, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
        return Result<KpiDto>.Success(_mapper.Map<KpiDto>(kpi));
    }
}
