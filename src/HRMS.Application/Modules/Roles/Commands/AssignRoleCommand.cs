using MediatR;
using HRMS.Application.Common.Interfaces;
using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Roles.Commands.CreateRole;
public record AssignRoleCommand(string UserId, string Role) : IRequest<Result>;
public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand, Result>
{
    private readonly IIdentityService _identityService;
    public AssignRoleCommandHandler(IIdentityService identityService) { _identityService = identityService; }
    public async Task<Result> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
        => await _identityService.AssignRoleAsync(request.UserId, request.Role, cancellationToken);
}
