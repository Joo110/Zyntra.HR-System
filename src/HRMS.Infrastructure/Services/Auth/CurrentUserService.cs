using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using HRMS.Application.Common.Interfaces;
namespace HRMS.Infrastructure.Services.Auth;
public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public CurrentUserService(IHttpContextAccessor httpContextAccessor) { _httpContextAccessor = httpContextAccessor; }
    public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? _httpContextAccessor.HttpContext?.User?.FindFirstValue("sub");
    public string? UserName => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
    public string? Email => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);
    public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    public IEnumerable<string> Roles => _httpContextAccessor.HttpContext?.User?.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value) ?? Enumerable.Empty<string>();
    public IEnumerable<string> Permissions => _httpContextAccessor.HttpContext?.User?.Claims.Where(c => c.Type == "permission").Select(c => c.Value) ?? Enumerable.Empty<string>();
    public bool HasPermission(string permission) => Permissions.Contains(permission);
    public bool HasRole(string role) => Roles.Contains(role);
}
