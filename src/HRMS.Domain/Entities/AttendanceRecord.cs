using HRMS.Domain.Enums;
namespace HRMS.Domain.Entities;
public class AttendanceRecord : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public DateTime Date { get; set; }
    public DateTime? CheckInTime { get; set; }
    public DateTime? CheckOutTime { get; set; }
    public AttendanceStatus Status { get; set; }
    public double? WorkingHours { get; set; }
    public double? OvertimeHours { get; set; }
    public string? Notes { get; set; }
    public string? CheckInLocation { get; set; }
    public string? CheckOutLocation { get; set; }
    public virtual Employee? Employee { get; set; }
}
