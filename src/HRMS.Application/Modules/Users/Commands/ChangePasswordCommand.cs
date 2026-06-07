using MediatR;
using HRMS.Application.Common.Interfaces;
using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Users.Commands.ChangePassword;
public record UserChangePasswordCommand(string UserId, string CurrentPassword, string NewPassword) : IRequest<Result>;
public class UserChangePasswordCommandHandler : IRequestHandler<UserChangePasswordCommand, Result>
{
    private readonly IAuthService _authService;
    public UserChangePasswordCommandHandler(IAuthService authService) { _authService = authService; }
    public async Task<Result> Handle(UserChangePasswordCommand request, CancellationToken cancellationToken)
        => await _authService.ChangePasswordAsync(request.UserId, request.CurrentPassword, request.NewPassword, cancellationToken);
}
