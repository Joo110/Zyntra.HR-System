using MediatR; using AutoMapper; using HRMS.Application.Common.Interfaces; using HRMS.Application.Modules.Attendance.DTOs; using HRMS.Domain.Entities; using HRMS.Domain.Enums; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Attendance.Commands.CheckIn;
public record CheckInCommand(Guid EmployeeId, DateTime CheckInTime, string? Location, string? Notes) : IRequest<Result<AttendanceDto>>;
public class CheckInCommandHandler : IRequestHandler<CheckInCommand, Result<AttendanceDto>>
{
    private readonly IUnitOfWork _uow; private readonly IMapper _mapper;
    public CheckInCommandHandler(IUnitOfWork uow, IMapper mapper) { _uow = uow; _mapper = mapper; }
    public async Task<Result<AttendanceDto>> Handle(CheckInCommand request, CancellationToken cancellationToken)
    {
        var existing = await _uow.Repository<AttendanceRecord>().FirstOrDefaultAsync(a => a.EmployeeId == request.EmployeeId && a.Date.Date == request.CheckInTime.Date && !a.IsDeleted, cancellationToken);
        if (existing != null) return Result<AttendanceDto>.Failure("Employee already checked in today.");
        var record = new AttendanceRecord { EmployeeId = request.EmployeeId, Date = request.CheckInTime.Date, CheckInTime = request.CheckInTime, Status = AttendanceStatus.Present, CheckInLocation = request.Location, Notes = request.Notes };
        await _uow.Repository<AttendanceRecord>().AddAsync(record, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
        return Result<AttendanceDto>.Success(_mapper.Map<AttendanceDto>(record));
    }
}
