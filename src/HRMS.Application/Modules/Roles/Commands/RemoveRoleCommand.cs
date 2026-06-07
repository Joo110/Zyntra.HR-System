using MediatR;
using HRMS.Application.Common.Interfaces;
using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Roles.Commands.DeleteRole;
public record RemoveRoleCommand(string UserId, string Role) : IRequest<Result>;
public class RemoveRoleCommandHandler : IRequestHandler<RemoveRoleCommand, Result>
{
    private readonly IIdentityService _identityService;
    public RemoveRoleCommandHandler(IIdentityService identityService) { _identityService = identityService; }
    public async Task<Result> Handle(RemoveRoleCommand request, CancellationToken cancellationToken)
        => await _identityService.RemoveRoleAsync(request.UserId, request.Role, cancellationToken);
}
