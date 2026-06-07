using AutoMapper; using HRMS.Application.Modules.Departments.DTOs; using HRMS.Domain.Entities;
namespace HRMS.Application.Modules.Departments.Mappings;
public class DepartmentMappingProfile : Profile
{
    public DepartmentMappingProfile()
    {
        CreateMap<Department, DepartmentDto>().ForMember(d => d.ParentDepartmentName, o => o.MapFrom(s => s.ParentDepartment != null ? s.ParentDepartment.Name : null)).ForMember(d => d.EmployeeCount, o => o.MapFrom(s => s.Employees.Count(e => !e.IsDeleted)));
    }
}
