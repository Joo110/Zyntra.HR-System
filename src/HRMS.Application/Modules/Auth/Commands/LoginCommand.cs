using MediatR;
using HRMS.Application.Modules.Auth.DTOs;
using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Auth.Commands.Login;
public record LoginCommand(string Email, string Password) : IRequest<Result<AuthResponseDto>>;
