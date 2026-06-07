using AutoMapper; using HRMS.Application.Modules.Branches.DTOs; using HRMS.Domain.Entities;
namespace HRMS.Application.Modules.Branches.Mappings;
public class BranchMappingProfile : Profile { public BranchMappingProfile() { CreateMap<Branch, BranchDto>(); } }
