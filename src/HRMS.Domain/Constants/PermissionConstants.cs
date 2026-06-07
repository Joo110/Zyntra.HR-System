namespace HRMS.Domain.Constants;
public static class PermissionConstants
{
    public static class Employees { public const string View="Employees.View"; public const string Create="Employees.Create"; public const string Update="Employees.Update"; public const string Delete="Employees.Delete"; public const string Export="Employees.Export"; }
    public static class Departments { public const string View="Departments.View"; public const string Create="Departments.Create"; public const string Update="Departments.Update"; public const string Delete="Departments.Delete"; }
    public static class Payroll { public const string View="Payroll.View"; public const string Process="Payroll.Process"; public const string Approve="Payroll.Approve"; }
    public static class Leave { public const string View="Leave.View"; public const string Request="Leave.Request"; public const string Approve="Leave.Approve"; }
    public static class Attendance { public const string View="Attendance.View"; public const string Manage="Attendance.Manage"; }
    public static class Recruitment { public const string View="Recruitment.View"; public const string Manage="Recruitment.Manage"; }
    public static class Reports { public const string View="Reports.View"; public const string Export="Reports.Export"; }
    public static class AuditLogs { public const string View="AuditLogs.View"; }
}
