using System.Security.Cryptography;
namespace HRMS.Shared.Helpers;
public static class PasswordHelper
{
    public static string GenerateRandomPassword(int length = 12)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%";
        return new string(Enumerable.Repeat(chars, length).Select(s => s[RandomNumberGenerator.GetInt32(s.Length)]).ToArray());
    }
}
