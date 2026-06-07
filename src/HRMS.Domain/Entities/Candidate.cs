namespace HRMS.Domain.Entities;
public class Candidate : BaseEntity
{
    public Guid JobVacancyId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? ResumeUrl { get; set; }
    public string? CoverLetter { get; set; }
    public string ApplicationStatus { get; set; } = "Applied";
    public DateTime ApplicationDate { get; set; } = DateTime.UtcNow;
    public virtual JobVacancy? JobVacancy { get; set; }
    public virtual ICollection<Interview> Interviews { get; set; } = new List<Interview>();
}
