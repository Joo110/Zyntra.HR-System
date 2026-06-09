using AutoMapper; 
using HRMS.Application.Common.Interfaces;
using HRMS.Application.Common.Localization;
using HRMS.Application.Modules.Attendance.DTOs; 
using HRMS.Domain.Entities; 
using HRMS.Shared.Models;
using MediatR;
using Microsoft.Extensions.Localization;
namespace HRMS.Application.Modules.Attendance.Commands.CheckOut;
public record CheckOutCommand(Guid AttendanceId, DateTime CheckOutTime, string? Location) : IRequest<Result<AttendanceDto>>;
public class CheckOutCommandHandler : IRequestHandler<CheckOutCommand, Result<AttendanceDto>>
{
    private readonly IUnitOfWork _uow; private readonly IMapper _mapper;
    private readonly ILocalizationService _localizer;
    public CheckOutCommandHandler(IUnitOfWork uow, IMapper mapper, ILocalizationService localizer)
    {
        _uow = uow; _mapper = mapper;
        _localizer = localizer;
    }
    public async Task<Result<AttendanceDto>> Handle(CheckOutCommand request, CancellationToken cancellationToken)
    {
        var record = await _uow.Repository<AttendanceRecord>().GetByIdAsync(request.AttendanceId, cancellationToken);
        if (record is null) return Result<AttendanceDto>.Failure(_localizer["Attendancerecordnotfound"]);
        if (record.CheckOutTime.HasValue) return Result<AttendanceDto>.Failure(_localizer["Alreadycheckedout"]);
        record.CheckOutTime = request.CheckOutTime; record.CheckOutLocation = request.Location;
        if (record.CheckInTime.HasValue) record.WorkingHours = (request.CheckOutTime - record.CheckInTime.Value).TotalHours;
        record.UpdatedAt = DateTime.UtcNow;
        _uow.Repository<AttendanceRecord>().Update(record);
        await _uow.SaveChangesAsync(cancellationToken);
        return Result<AttendanceDto>.Success(_mapper.Map<AttendanceDto>(record));
    }
}
