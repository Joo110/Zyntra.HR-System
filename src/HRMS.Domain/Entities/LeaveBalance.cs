namespace HRMS.Domain.Entities;
public class LeaveBalance : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public Guid LeaveTypeId { get; set; }
    public int Year { get; set; }
    public double Allocated { get; set; }
    public double Used { get; set; }
    public double Pending { get; set; }
    public double Remaining => Allocated - Used - Pending;
    public virtual Employee? Employee { get; set; }
    public virtual LeaveType? LeaveType { get; set; }
}
