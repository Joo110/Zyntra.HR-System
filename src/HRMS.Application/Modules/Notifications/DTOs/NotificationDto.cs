using HRMS.Domain.Enums;
namespace HRMS.Application.Modules.Notifications.DTOs;
public class NotificationDto { public Guid Id { get; set; } public string Title { get; set; } = string.Empty; public string Message { get; set; } = string.Empty; public NotificationType Type { get; set; } public string? RecipientId { get; set; } public bool IsRead { get; set; } public DateTime? ReadAt { get; set; } public bool IsSent { get; set; } public DateTime? SentAt { get; set; } public DateTime CreatedAt { get; set; } }
public class SendNotificationDto { public string RecipientId { get; set; } = string.Empty; public string Title { get; set; } = string.Empty; public string Message { get; set; } = string.Empty; public NotificationType Type { get; set; } = NotificationType.InApp; }
