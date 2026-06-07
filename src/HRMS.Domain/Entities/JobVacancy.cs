using HRMS.Domain.Enums;
namespace HRMS.Domain.Entities;
public class JobVacancy : BaseEntity
{
    public string Code { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid? DepartmentId { get; set; }
    public Guid? PositionId { get; set; }
    public int NumberOfPositions { get; set; } = 1;
    public DateTime PostedDate { get; set; }
    public DateTime? ClosingDate { get; set; }
    public RecruitmentStatus Status { get; set; } = RecruitmentStatus.Open;
    public decimal? MinSalary { get; set; }
    public decimal? MaxSalary { get; set; }
    public string? Requirements { get; set; }
    public virtual Department? Department { get; set; }
    public virtual ICollection<Candidate> Candidates { get; set; } = new List<Candidate>();
}
