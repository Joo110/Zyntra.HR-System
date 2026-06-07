using MediatR;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.Modules.Users.DTOs;
using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Users.Commands.CreateUser;
public record CreateUserCommand(string Email, string Password, string FirstName, string LastName, IEnumerable<string>? Roles) : IRequest<Result<string>>;
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<string>>
{
    private readonly IIdentityService _identityService;
    public CreateUserCommandHandler(IIdentityService identityService) { _identityService = identityService; }
    public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        => await _identityService.CreateUserAsync(request.Email, request.Password, request.FirstName, request.LastName, request.Roles, cancellationToken);
}
