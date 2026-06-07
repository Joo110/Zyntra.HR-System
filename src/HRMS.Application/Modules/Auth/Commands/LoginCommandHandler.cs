using MediatR;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.Modules.Auth.DTOs;
using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Auth.Commands.Login;
public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<AuthResponseDto>>
{
    private readonly IAuthService _authService;
    public LoginCommandHandler(IAuthService authService) { _authService = authService; }
    public async Task<Result<AuthResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        => await _authService.LoginAsync(request, cancellationToken);
}
