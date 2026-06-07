using MediatR; using AutoMapper; using HRMS.Application.Common.Interfaces; using HRMS.Application.Modules.Payroll.DTOs; using HRMS.Domain.Entities; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Payroll.Queries.GetAllPayrolls;
public record GetAllPayrollsQuery(int PageNumber = 1, int PageSize = 20) : IRequest<Result<PagedResult<PayrollBatchDto>>>;
public class GetAllPayrollsQueryHandler : IRequestHandler<GetAllPayrollsQuery, Result<PagedResult<PayrollBatchDto>>>
{
    private readonly IUnitOfWork _uow; private readonly IMapper _mapper;
    public GetAllPayrollsQueryHandler(IUnitOfWork uow, IMapper mapper) { _uow = uow; _mapper = mapper; }
    public async Task<Result<PagedResult<PayrollBatchDto>>> Handle(GetAllPayrollsQuery request, CancellationToken cancellationToken)
    {
        var query = _uow.Repository<PayrollBatch>().GetQueryable().Where(p => !p.IsDeleted).OrderByDescending(p => p.Year).ThenByDescending(p => p.Month);
        var total = query.Count();
        var items = query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();
        return Result<PagedResult<PayrollBatchDto>>.Success(PagedResult<PayrollBatchDto>.Create(_mapper.Map<IEnumerable<PayrollBatchDto>>(items), total, request.PageNumber, request.PageSize));
    }
}
