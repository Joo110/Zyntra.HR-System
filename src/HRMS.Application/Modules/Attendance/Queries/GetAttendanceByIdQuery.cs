using MediatR; using AutoMapper; using HRMS.Application.Common.Interfaces; using HRMS.Application.Modules.Attendance.DTOs; using HRMS.Domain.Entities; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Attendance.Queries.GetAttendanceById;
public record GetAttendanceByIdQuery(Guid Id) : IRequest<Result<AttendanceDto>>;
public class GetAttendanceByIdQueryHandler : IRequestHandler<GetAttendanceByIdQuery, Result<AttendanceDto>>
{
    private readonly IUnitOfWork _uow; private readonly IMapper _mapper;
    public GetAttendanceByIdQueryHandler(IUnitOfWork uow, IMapper mapper) { _uow = uow; _mapper = mapper; }
    public async Task<Result<AttendanceDto>> Handle(GetAttendanceByIdQuery request, CancellationToken cancellationToken)
    {
        var r = await _uow.Repository<AttendanceRecord>().GetByIdAsync(request.Id, cancellationToken);
        if (r is null || r.IsDeleted) return Result<AttendanceDto>.Failure("Attendance record not found.");
        return Result<AttendanceDto>.Success(_mapper.Map<AttendanceDto>(r));
    }
}
