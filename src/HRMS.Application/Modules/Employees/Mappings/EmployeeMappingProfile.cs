using AutoMapper;
using HRMS.Application.Modules.Employees.DTOs;
using HRMS.Domain.Entities;
namespace HRMS.Application.Modules.Employees.Mappings;
public class EmployeeMappingProfile : Profile
{
    public EmployeeMappingProfile()
    {
        CreateMap<Employee, EmployeeDto>()
            .ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.Department != null ? s.Department.Name : null))
            .ForMember(d => d.PositionTitle, o => o.MapFrom(s => s.Position != null ? s.Position.Title : null))
            .ForMember(d => d.BranchName, o => o.MapFrom(s => s.Branch != null ? s.Branch.Name : null))
            .ForMember(d => d.ManagerName, o => o.MapFrom(s => s.Manager != null ? $"{s.Manager.FirstName} {s.Manager.LastName}" : null));
        CreateMap<CreateEmployeeDto, Employee>();
    }
}
