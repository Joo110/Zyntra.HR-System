using Microsoft.AspNetCore.Http;
using HRMS.Application.Common.Interfaces;
using HRMS.Domain.Entities;
using HRMS.Domain.Enums;
using HRMS.Infrastructure.Persistence;
namespace HRMS.Infrastructure.Services.Audit;
public class AuditService : IAuditService
{
    private readonly HrmsDbContext _context;
    private readonly ICurrentUserService _currentUser;
    private readonly IHttpContextAccessor _httpContext;
    public AuditService(HrmsDbContext context, ICurrentUserService currentUser, IHttpContextAccessor httpContext) { _context = context; _currentUser = currentUser; _httpContext = httpContext; }
    public async Task LogAsync(AuditAction action, string entityName, string? entityId = null, string? oldValues = null, string? newValues = null, CancellationToken cancellationToken = default)
    {
        var log = new AuditLog { UserId = _currentUser.UserId, UserName = _currentUser.UserName, Action = action, EntityName = entityName, EntityId = entityId, OldValues = oldValues, NewValues = newValues, IpAddress = _httpContext.HttpContext?.Connection?.RemoteIpAddress?.ToString(), UserAgent = _httpContext.HttpContext?.Request?.Headers["User-Agent"].ToString(), Timestamp = DateTime.UtcNow };
        _context.AuditLogs.Add(log);
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task LogLoginAsync(string userId, string userName, bool isSuccess, string? errorMessage = null, CancellationToken cancellationToken = default)
    {
        var log = new AuditLog { UserId = userId, UserName = userName, Action = AuditAction.Login, EntityName = "Auth", IsSuccess = isSuccess, ErrorMessage = errorMessage, IpAddress = _httpContext.HttpContext?.Connection?.RemoteIpAddress?.ToString(), Timestamp = DateTime.UtcNow };
        _context.AuditLogs.Add(log);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
