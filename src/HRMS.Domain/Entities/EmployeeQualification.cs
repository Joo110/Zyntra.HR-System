namespace HRMS.Domain.Entities;
public class EmployeeQualification : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public string Degree { get; set; } = string.Empty;
    public string Institution { get; set; } = string.Empty;
    public string? FieldOfStudy { get; set; }
    public int? YearCompleted { get; set; }
    public string? Grade { get; set; }
    public virtual Employee? Employee { get; set; }
}
