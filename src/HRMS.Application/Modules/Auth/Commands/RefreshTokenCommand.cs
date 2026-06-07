using MediatR;
using HRMS.Application.Modules.Auth.DTOs;
using HRMS.Shared.Models;
using HRMS.Application.Common.Interfaces;
namespace HRMS.Application.Modules.Auth.Commands.RefreshToken;
public record RefreshTokenCommand(string RefreshToken) : IRequest<Result<AuthResponseDto>>;
public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<AuthResponseDto>>
{
    private readonly IAuthService _authService;
    public RefreshTokenCommandHandler(IAuthService authService) { _authService = authService; }
    public async Task<Result<AuthResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        => await _authService.RefreshTokenAsync(request.RefreshToken, cancellationToken);
}
