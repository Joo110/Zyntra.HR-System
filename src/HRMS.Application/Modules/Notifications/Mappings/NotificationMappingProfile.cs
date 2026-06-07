using AutoMapper; using HRMS.Application.Modules.Notifications.DTOs; using HRMS.Domain.Entities;
namespace HRMS.Application.Modules.Notifications.Mappings;
public class NotificationMappingProfile : Profile { public NotificationMappingProfile() { CreateMap<Notification, NotificationDto>(); } }
