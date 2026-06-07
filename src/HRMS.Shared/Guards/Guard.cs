namespace HRMS.Shared.Guards;
public static class Guard
{
    public static T NotNull<T>(T? value, string paramName) where T : class { if (value is null) throw new ArgumentNullException(paramName); return value; }
    public static string NotNullOrEmpty(string? value, string paramName) { if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException($"{paramName} cannot be null or empty."); return value; }
    public static Guid NotEmpty(Guid value, string paramName) { if (value == Guid.Empty) throw new ArgumentException($"{paramName} cannot be empty Guid."); return value; }
}
