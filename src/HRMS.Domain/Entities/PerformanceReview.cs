using HRMS.Domain.Enums;
namespace HRMS.Domain.Entities;
public class PerformanceReview : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public int ReviewYear { get; set; }
    public int? ReviewQuarter { get; set; }
    public string ReviewPeriod { get; set; } = string.Empty;
    public PerformanceRating? SelfRating { get; set; }
    public PerformanceRating? ManagerRating { get; set; }
    public PerformanceRating? FinalRating { get; set; }
    public string? SelfComments { get; set; }
    public string? ManagerComments { get; set; }
    public string Status { get; set; } = "Draft";
    public virtual Employee? Employee { get; set; }
}
