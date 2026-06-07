using HRMS.Domain.Enums;
namespace HRMS.Application.Common.Interfaces;
public interface INotificationService
{
    Task SendAsync(string recipientId, string title, string message, NotificationType type = NotificationType.InApp, CancellationToken cancellationToken = default);
    Task SendEmailAsync(string email, string subject, string body, CancellationToken cancellationToken = default);
    Task SendSmsAsync(string phone, string message, CancellationToken cancellationToken = default);
}
