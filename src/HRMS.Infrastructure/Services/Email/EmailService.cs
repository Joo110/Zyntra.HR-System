using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using HRMS.Application.Common.Interfaces;
namespace HRMS.Infrastructure.Services.Email;
public class EmailService : IEmailService
{
    private readonly IConfiguration _config;
    public EmailService(IConfiguration config) { _config = config; }
    public async Task SendAsync(string to, string subject, string body, bool isHtml = true, CancellationToken cancellationToken = default)
    {
        var message = new MimeMessage();
        message.From.Add(MailboxAddress.Parse(_config["Email:From"]));
        message.To.Add(MailboxAddress.Parse(to));
        message.Subject = subject;
        var bodyBuilder = new BodyBuilder();
        if (isHtml) bodyBuilder.HtmlBody = body; else bodyBuilder.TextBody = body;
        message.Body = bodyBuilder.ToMessageBody();
        using var client = new SmtpClient();
        await client.ConnectAsync(_config["Email:Host"], int.Parse(_config["Email:Port"] ?? "587"), SecureSocketOptions.StartTls, cancellationToken);
        await client.AuthenticateAsync(_config["Email:Username"], _config["Email:Password"], cancellationToken);
        await client.SendAsync(message, cancellationToken);
        await client.DisconnectAsync(true, cancellationToken);
    }
}
