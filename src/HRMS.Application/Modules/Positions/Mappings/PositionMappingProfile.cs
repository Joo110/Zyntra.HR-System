using AutoMapper; using HRMS.Application.Modules.Positions.DTOs; using HRMS.Domain.Entities;
namespace HRMS.Application.Modules.Positions.Mappings;
public class PositionMappingProfile : Profile
{
    public PositionMappingProfile() { CreateMap<Position, PositionDto>().ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.Department != null ? s.Department.Name : null)); }
}
