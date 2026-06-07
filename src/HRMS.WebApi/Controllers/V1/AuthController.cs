using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HRMS.Application.Modules.Auth.Commands.ChangePassword;
using HRMS.Application.Modules.Auth.Commands.ForgotPassword;
using HRMS.Application.Modules.Auth.Commands.Login;
using HRMS.Application.Modules.Auth.Commands.RefreshToken;
using HRMS.Application.Modules.Auth.Commands.ResetPassword;

namespace HRMS.WebApi.Controllers.V1;

[ApiVersion("1.0")]
public class AuthController : ApiBaseController
{
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken ct)
        => HandleResult(await Mediator.Send(command, ct));

    [HttpPost("refresh-token")]
    [AllowAnonymous]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command, CancellationToken ct)
        => HandleResult(await Mediator.Send(command, ct));

    [HttpPost("change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command, CancellationToken ct)
        => HandleResult(await Mediator.Send(command, ct));

    [HttpPost("forgot-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command, CancellationToken ct)
        => HandleResult(await Mediator.Send(command, ct));

    [HttpPost("reset-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command, CancellationToken ct)
        => HandleResult(await Mediator.Send(command, ct));
}
