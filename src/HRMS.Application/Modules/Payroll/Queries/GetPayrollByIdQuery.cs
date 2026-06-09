using MediatR; using AutoMapper; using HRMS.Application.Common.Interfaces; using HRMS.Application.Modules.Payroll.DTOs; using HRMS.Domain.Entities; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Payroll.Queries.GetPayrollById;
public record GetPayrollByIdQuery(Guid Id) : IRequest<Result<PayrollBatchDto>>;
public class GetPayrollByIdQueryHandler : IRequestHandler<GetPayrollByIdQuery, Result<PayrollBatchDto>>
{
    private readonly IUnitOfWork _uow; private readonly IMapper _mapper;
    private ILocalizationService _localizer;
    public GetPayrollByIdQueryHandler(IUnitOfWork uow, IMapper mapper, ILocalizationService localizer)
    {
        _uow = uow; _mapper = mapper;
        _localizer = localizer;
    }
    public async Task<Result<PayrollBatchDto>> Handle(GetPayrollByIdQuery request, CancellationToken cancellationToken)
    {
        var b = await _uow.Repository<PayrollBatch>().GetByIdAsync(request.Id, cancellationToken);
        if (b is null || b.IsDeleted) return Result<PayrollBatchDto>.Failure(_localizer["PayrollBatchNotFound"]);
        return Result<PayrollBatchDto>.Success(_mapper.Map<PayrollBatchDto>(b));
    }
}
