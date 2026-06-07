using MediatR; using HRMS.Application.Modules.Permissions.DTOs; using HRMS.Domain.Constants; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Permissions.Queries.GetAllPermissions;
public record GetAllPermissionsQuery : IRequest<Result<IEnumerable<PermissionDto>>>;
public class GetAllPermissionsQueryHandler : IRequestHandler<GetAllPermissionsQuery, Result<IEnumerable<PermissionDto>>>
{
    public Task<Result<IEnumerable<PermissionDto>>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
    {
        var permissions = typeof(PermissionConstants).GetNestedTypes()
            .SelectMany(t => t.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.FlattenHierarchy)
                .Where(f => f.IsLiteral && !f.IsInitOnly && f.FieldType == typeof(string))
                .Select(f => new PermissionDto { Name = (string)f.GetValue(null)!, Module = t.Name, Action = f.Name }));
        return Task.FromResult(Result<IEnumerable<PermissionDto>>.Success(permissions));
    }
}
