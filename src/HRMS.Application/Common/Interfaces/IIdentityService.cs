using HRMS.Shared.Models;
namespace HRMS.Application.Common.Interfaces;
public interface IIdentityService
{
    Task<Result<string>> CreateUserAsync(string email, string password, string firstName, string lastName, IEnumerable<string>? roles = null, CancellationToken cancellationToken = default);
    Task<Result> UpdateUserAsync(string userId, string? firstName = null, string? lastName = null, string? email = null, CancellationToken cancellationToken = default);
    Task<Result> DeleteUserAsync(string userId, CancellationToken cancellationToken = default);
    Task<Result> AssignRoleAsync(string userId, string role, CancellationToken cancellationToken = default);
    Task<Result> RemoveRoleAsync(string userId, string role, CancellationToken cancellationToken = default);
    Task<IEnumerable<string>> GetUserRolesAsync(string userId, CancellationToken cancellationToken = default);
    Task<bool> UserExistsAsync(string userId, CancellationToken cancellationToken = default);
}
