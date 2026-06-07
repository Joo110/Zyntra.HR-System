using Microsoft.AspNetCore.Identity;
using HRMS.Application.Common.Interfaces;
using HRMS.Shared.Models;
namespace HRMS.Infrastructure.Identity;
public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    public IdentityService(UserManager<ApplicationUser> userManager) { _userManager = userManager; }
    public async Task<Result<string>> CreateUserAsync(string email, string password, string firstName, string lastName, IEnumerable<string>? roles = null, CancellationToken cancellationToken = default)
    {
        var user = new ApplicationUser { UserName = email, Email = email, FirstName = firstName, LastName = lastName, EmailConfirmed = true };
        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded) return Result<string>.Failure(result.Errors.Select(e => e.Description));
        if (roles != null) foreach (var role in roles) await _userManager.AddToRoleAsync(user, role);
        return Result<string>.Success(user.Id);
    }
    public async Task<Result> UpdateUserAsync(string userId, string? firstName = null, string? lastName = null, string? email = null, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return Result.Failure("User not found.");
        if (firstName != null) user.FirstName = firstName;
        if (lastName != null) user.LastName = lastName;
        if (email != null) { user.Email = email; user.UserName = email; }
        var result = await _userManager.UpdateAsync(user);
        return result.Succeeded ? Result.Success() : Result.Failure(result.Errors.Select(e => e.Description));
    }
    public async Task<Result> DeleteUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return Result.Failure("User not found.");
        var result = await _userManager.DeleteAsync(user);
        return result.Succeeded ? Result.Success() : Result.Failure(result.Errors.Select(e => e.Description));
    }
    public async Task<Result> AssignRoleAsync(string userId, string role, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return Result.Failure("User not found.");
        var result = await _userManager.AddToRoleAsync(user, role);
        return result.Succeeded ? Result.Success() : Result.Failure(result.Errors.Select(e => e.Description));
    }
    public async Task<Result> RemoveRoleAsync(string userId, string role, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return Result.Failure("User not found.");
        var result = await _userManager.RemoveFromRoleAsync(user, role);
        return result.Succeeded ? Result.Success() : Result.Failure(result.Errors.Select(e => e.Description));
    }
    public async Task<IEnumerable<string>> GetUserRolesAsync(string userId, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return user == null ? Enumerable.Empty<string>() : await _userManager.GetRolesAsync(user);
    }
    public async Task<bool> UserExistsAsync(string userId, CancellationToken cancellationToken = default)
        => await _userManager.FindByIdAsync(userId) != null;
}
