using AutoMapper; using HRMS.Application.Modules.AuditLogs.DTOs; using HRMS.Domain.Entities;
namespace HRMS.Application.Modules.AuditLogs.Mappings;
public class AuditLogMappingProfile : Profile { public AuditLogMappingProfile() { CreateMap<AuditLog, AuditLogDto>(); } }
