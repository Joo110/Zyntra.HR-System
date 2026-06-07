namespace HRMS.Shared.Extensions;
public static class DateTimeExtensions
{
    public static int GetAge(this DateTime dateOfBirth) => DateTime.UtcNow.Year - dateOfBirth.Year - (DateTime.UtcNow.DayOfYear < dateOfBirth.DayOfYear ? 1 : 0);
    public static bool IsWorkingDay(this DateTime date) => date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
}
