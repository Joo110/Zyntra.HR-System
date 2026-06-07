namespace HRMS.Application.Modules.Users.DTOs;
public class UserDto
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";
    public bool IsActive { get; set; }
    public IEnumerable<string> Roles { get; set; } = Enumerable.Empty<string>();
    public DateTime CreatedAt { get; set; }
}
public class CreateUserDto { public string Email { get; set; } = string.Empty; public string Password { get; set; } = string.Empty; public string FirstName { get; set; } = string.Empty; public string LastName { get; set; } = string.Empty; public IEnumerable<string>? Roles { get; set; } }
public class UpdateUserDto { public string? FirstName { get; set; } public string? LastName { get; set; } public string? Email { get; set; } }
