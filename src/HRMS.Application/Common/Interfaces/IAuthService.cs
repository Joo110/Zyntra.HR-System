using HRMS.Application.Modules.Auth.Commands.Login;
using HRMS.Application.Modules.Auth.DTOs;
using HRMS.Shared.Models;
namespace HRMS.Application.Common.Interfaces;
public interface IAuthService
{
    Task<Result<AuthResponseDto>> LoginAsync(LoginCommand command, CancellationToken cancellationToken = default);
    Task<Result> LogoutAsync(string userId, CancellationToken cancellationToken = default);
    Task<Result<AuthResponseDto>> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
    Task<Result> ChangePasswordAsync(string userId, string currentPassword, string newPassword, CancellationToken cancellationToken = default);
    Task<Result> ForgotPasswordAsync(string email, CancellationToken cancellationToken = default);
    Task<Result> ResetPasswordAsync(string email, string token, string newPassword, CancellationToken cancellationToken = default);
}
