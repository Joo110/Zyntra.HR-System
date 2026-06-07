namespace HRMS.Shared.Helpers;
public static class CodeGenerator
{
    public static string GenerateCode(string prefix = "") => $"{prefix}{Guid.NewGuid().ToString("N")[..8].ToUpper()}";
}
