namespace HRMS.Application.Modules.Roles.DTOs;
public class RoleDto { public string Id { get; set; } = string.Empty; public string Name { get; set; } = string.Empty; public string? Description { get; set; } public IEnumerable<string> Permissions { get; set; } = Enumerable.Empty<string>(); }
public class CreateRoleDto { public string Name { get; set; } = string.Empty; public string? Description { get; set; } public IEnumerable<string> Permissions { get; set; } = Enumerable.Empty<string>(); }
