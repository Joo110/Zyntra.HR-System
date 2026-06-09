using MediatR; using AutoMapper; using HRMS.Application.Common.Interfaces; using HRMS.Application.Modules.Payroll.DTOs; using HRMS.Domain.Entities; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Payroll.Queries.GetPayslip;
public record GetPayslipQuery(Guid EmployeeId, int Month, int Year) : IRequest<Result<PayslipDto>>;
public class GetPayslipQueryHandler : IRequestHandler<GetPayslipQuery, Result<PayslipDto>>
{
    private readonly IUnitOfWork _uow; private readonly IMapper _mapper;
    private ILocalizationService _localizer;
    public GetPayslipQueryHandler(IUnitOfWork uow, IMapper mapper, ILocalizationService localizer)
    {
        _uow = uow; _mapper = mapper;
        _localizer = localizer;
    }
    public async Task<Result<PayslipDto>> Handle(GetPayslipQuery request, CancellationToken cancellationToken)
    {
        var slip = await _uow.Repository<Payslip>().FirstOrDefaultAsync(p => p.EmployeeId == request.EmployeeId && p.Month == request.Month && p.Year == request.Year && !p.IsDeleted, cancellationToken);
        if (slip is null) return Result<PayslipDto>.Failure(_localizer["PayslipNotFound"]);
        return Result<PayslipDto>.Success(_mapper.Map<PayslipDto>(slip));
    }
}
