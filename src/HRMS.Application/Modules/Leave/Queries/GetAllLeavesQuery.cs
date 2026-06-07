using MediatR; using AutoMapper; using HRMS.Application.Common.Interfaces; using HRMS.Application.Modules.Leave.DTOs; using HRMS.Domain.Entities; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Leave.Queries.GetAllLeaves;
public record GetAllLeavesQuery(Guid? EmployeeId, int PageNumber = 1, int PageSize = 20) : IRequest<Result<PagedResult<LeaveRequestDto>>>;
public class GetAllLeavesQueryHandler : IRequestHandler<GetAllLeavesQuery, Result<PagedResult<LeaveRequestDto>>>
{
    private readonly IUnitOfWork _uow; private readonly IMapper _mapper;
    public GetAllLeavesQueryHandler(IUnitOfWork uow, IMapper mapper) { _uow = uow; _mapper = mapper; }
    public async Task<Result<PagedResult<LeaveRequestDto>>> Handle(GetAllLeavesQuery request, CancellationToken cancellationToken)
    {
        var query = _uow.Repository<LeaveRequest>().GetQueryable().Where(l => !l.IsDeleted);
        if (request.EmployeeId.HasValue) query = query.Where(l => l.EmployeeId == request.EmployeeId.Value);
        var total = query.Count();
        var items = query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();
        return Result<PagedResult<LeaveRequestDto>>.Success(PagedResult<LeaveRequestDto>.Create(_mapper.Map<IEnumerable<LeaveRequestDto>>(items), total, request.PageNumber, request.PageSize));
    }
}
