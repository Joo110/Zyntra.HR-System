using AutoMapper; using HRMS.Application.Modules.Leave.DTOs; using HRMS.Domain.Entities;
namespace HRMS.Application.Modules.Leave.Mappings;
public class LeaveMappingProfile : Profile
{
    public LeaveMappingProfile()
    {
        CreateMap<LeaveRequest, LeaveRequestDto>().ForMember(d => d.EmployeeName, o => o.MapFrom(s => s.Employee != null ? $"{s.Employee.FirstName} {s.Employee.LastName}" : null)).ForMember(d => d.LeaveTypeName, o => o.MapFrom(s => s.LeaveType != null ? s.LeaveType.Name : null));
        CreateMap<LeaveType, LeaveTypeDto>();
        CreateMap<LeaveBalance, LeaveBalanceDto>().ForMember(d => d.LeaveTypeName, o => o.MapFrom(s => s.LeaveType != null ? s.LeaveType.Name : null)).ForMember(d => d.Remaining, o => o.MapFrom(s => s.Remaining));
    }
}
