using HRMS.Application.Common.Interfaces;
using HRMS.Domain.Enums;
using Microsoft.Extensions.Logging;
namespace HRMS.Infrastructure.Services.Notification;
public class NotificationService : INotificationService
{
    private readonly IEmailService _emailService;
    private readonly ILogger<NotificationService> _logger;
    public NotificationService(IEmailService emailService, ILogger<NotificationService> logger) { _emailService = emailService; _logger = logger; }
    public async Task SendAsync(string recipientId, string title, string message, NotificationType type = NotificationType.InApp, CancellationToken cancellationToken = default)
    {
        switch (type)
        {
            case NotificationType.Email: _logger.LogInformation("Sending email to recipient {RecipientId}: {Title}", recipientId, title); break;
            case NotificationType.SMS: _logger.LogInformation("Sending SMS to recipient {RecipientId}: {Message}", recipientId, message); break;
            case NotificationType.Push: _logger.LogInformation("Sending push notification to recipient {RecipientId}: {Title}", recipientId, title); break;
            case NotificationType.InApp: _logger.LogInformation("In-app notification stored for {RecipientId}: {Title}", recipientId, title); break;
        }
        await Task.CompletedTask;
    }
    public async Task SendEmailAsync(string email, string subject, string body, CancellationToken cancellationToken = default)
        => await _emailService.SendAsync(email, subject, body, true, cancellationToken);
    public async Task SendSmsAsync(string phone, string message, CancellationToken cancellationToken = default)
    { _logger.LogInformation("SMS to {Phone}: {Message}", phone, message); await Task.CompletedTask; }
}
