using MediatR;
using HRMS.Application.Common.Interfaces;
using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Auth.Commands.ChangePassword;
public record ChangePasswordCommand(string UserId, string CurrentPassword, string NewPassword) : IRequest<Result>;
public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result>
{
    private readonly IAuthService _authService;
    public ChangePasswordCommandHandler(IAuthService authService) { _authService = authService; }
    public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        => await _authService.ChangePasswordAsync(request.UserId, request.CurrentPassword, request.NewPassword, cancellationToken);
}
