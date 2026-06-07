using HRMS.Domain.Enums;
namespace HRMS.Application.Modules.Attendance.DTOs;
public class AttendanceDto { public Guid Id { get; set; } public Guid EmployeeId { get; set; } public string? EmployeeName { get; set; } public DateTime Date { get; set; } public DateTime? CheckInTime { get; set; } public DateTime? CheckOutTime { get; set; } public AttendanceStatus Status { get; set; } public double? WorkingHours { get; set; } public double? OvertimeHours { get; set; } public string? Notes { get; set; } public DateTime CreatedAt { get; set; } }
public class CheckInDto { public Guid EmployeeId { get; set; } public DateTime CheckInTime { get; set; } = DateTime.UtcNow; public string? Location { get; set; } public string? Notes { get; set; } }
public class CheckOutDto { public Guid AttendanceId { get; set; } public DateTime CheckOutTime { get; set; } = DateTime.UtcNow; public string? Location { get; set; } }
