using HRMS.Domain.Enums;
namespace HRMS.Domain.Entities;
public class Shift : BaseEntity
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public ShiftType Type { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public double WorkingHours { get; set; }
    public bool IsActive { get; set; } = true;
}
