using AutoMapper; using HRMS.Application.Modules.Attendance.DTOs; using HRMS.Domain.Entities;
namespace HRMS.Application.Modules.Attendance.Mappings;
public class AttendanceMappingProfile : Profile
{
    public AttendanceMappingProfile() { CreateMap<AttendanceRecord, AttendanceDto>().ForMember(d => d.EmployeeName, o => o.MapFrom(s => s.Employee != null ? $"{s.Employee.FirstName} {s.Employee.LastName}" : null)); }
}
