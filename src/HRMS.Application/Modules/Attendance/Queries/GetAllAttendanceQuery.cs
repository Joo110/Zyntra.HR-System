using MediatR; using AutoMapper; using HRMS.Application.Common.Interfaces; using HRMS.Application.Modules.Attendance.DTOs; using HRMS.Domain.Entities; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Attendance.Queries.GetAllAttendance;
public record GetAllAttendanceQuery(Guid? EmployeeId, DateTime? From, DateTime? To, int PageNumber = 1, int PageSize = 20) : IRequest<Result<PagedResult<AttendanceDto>>>;
public class GetAllAttendanceQueryHandler : IRequestHandler<GetAllAttendanceQuery, Result<PagedResult<AttendanceDto>>>
{
    private readonly IUnitOfWork _uow; private readonly IMapper _mapper;
    public GetAllAttendanceQueryHandler(IUnitOfWork uow, IMapper mapper) { _uow = uow; _mapper = mapper; }
    public async Task<Result<PagedResult<AttendanceDto>>> Handle(GetAllAttendanceQuery request, CancellationToken cancellationToken)
    {
        var query = _uow.Repository<AttendanceRecord>().GetQueryable().Where(a => !a.IsDeleted);
        if (request.EmployeeId.HasValue) query = query.Where(a => a.EmployeeId == request.EmployeeId.Value);
        if (request.From.HasValue) query = query.Where(a => a.Date >= request.From.Value);
        if (request.To.HasValue) query = query.Where(a => a.Date <= request.To.Value);
        var total = query.Count();
        var items = query.OrderByDescending(a => a.Date).Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();
        return Result<PagedResult<AttendanceDto>>.Success(PagedResult<AttendanceDto>.Create(_mapper.Map<IEnumerable<AttendanceDto>>(items), total, request.PageNumber, request.PageSize));
    }
}
