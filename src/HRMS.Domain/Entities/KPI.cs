namespace HRMS.Domain.Entities;
public class KPI : BaseEntity
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid? DepartmentId { get; set; }
    public decimal TargetValue { get; set; }
    public string? Unit { get; set; }
    public int Weight { get; set; } = 1;
    public bool IsActive { get; set; } = true;
    public virtual Department? Department { get; set; }
}
