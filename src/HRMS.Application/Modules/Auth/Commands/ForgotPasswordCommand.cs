using MediatR;
using HRMS.Application.Common.Interfaces;
using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Auth.Commands.ForgotPassword;
public record ForgotPasswordCommand(string Email) : IRequest<Result>;
public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Result>
{
    private readonly IAuthService _authService;
    public ForgotPasswordCommandHandler(IAuthService authService) { _authService = authService; }
    public async Task<Result> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        => await _authService.ForgotPasswordAsync(request.Email, cancellationToken);
}
