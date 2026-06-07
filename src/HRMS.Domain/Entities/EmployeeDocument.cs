namespace HRMS.Domain.Entities;
public class EmployeeDocument : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public string DocumentType { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public string? Description { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public virtual Employee? Employee { get; set; }
}
