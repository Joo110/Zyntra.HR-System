using AutoMapper; using HRMS.Application.Modules.Performance.DTOs; using HRMS.Domain.Entities;
namespace HRMS.Application.Modules.Performance.Mappings;
public class PerformanceMappingProfile : Profile
{
    public PerformanceMappingProfile()
    {
        CreateMap<KPI, KpiDto>().ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.Department != null ? s.Department.Name : null));
        CreateMap<PerformanceReview, PerformanceReviewDto>().ForMember(d => d.EmployeeName, o => o.MapFrom(s => s.Employee != null ? $"{s.Employee.FirstName} {s.Employee.LastName}" : null));
    }
}
