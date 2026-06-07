using MediatR;
using HRMS.Application.Common.Interfaces;
using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Users.Commands.DeleteUser;
public record DeleteUserCommand(string UserId) : IRequest<Result>;
public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result>
{
    private readonly IIdentityService _identityService;
    public DeleteUserCommandHandler(IIdentityService identityService) { _identityService = identityService; }
    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        => await _identityService.DeleteUserAsync(request.UserId, cancellationToken);
}
