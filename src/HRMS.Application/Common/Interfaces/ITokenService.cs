namespace HRMS.Application.Common.Interfaces;
public interface ITokenService
{
    Task<string> GenerateAccessTokenAsync(string userId, string email, IEnumerable<string> roles, IEnumerable<string> permissions);
    Task<string> GenerateRefreshTokenAsync();
    bool IsTokenExpired(string token);
}
