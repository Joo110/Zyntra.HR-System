using HRMS.Domain.Enums;
namespace HRMS.Application.Common.Interfaces;
public interface IAuditService
{
    Task LogAsync(AuditAction action, string entityName, string? entityId = null, string? oldValues = null, string? newValues = null, CancellationToken cancellationToken = default);
    Task LogLoginAsync(string userId, string userName, bool isSuccess, string? errorMessage = null, CancellationToken cancellationToken = default);
}
