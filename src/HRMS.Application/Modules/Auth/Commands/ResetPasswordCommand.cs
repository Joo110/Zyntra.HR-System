using MediatR;
using HRMS.Application.Common.Interfaces;
using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Auth.Commands.ResetPassword;
public record ResetPasswordCommand(string Email, string Token, string NewPassword) : IRequest<Result>;
public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result>
{
    private readonly IAuthService _authService;
    public ResetPasswordCommandHandler(IAuthService authService) { _authService = authService; }
    public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        => await _authService.ResetPasswordAsync(request.Email, request.Token, request.NewPassword, cancellationToken);
}
