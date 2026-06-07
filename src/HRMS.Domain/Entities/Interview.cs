namespace HRMS.Domain.Entities;
public class Interview : BaseEntity
{
    public Guid CandidateId { get; set; }
    public DateTime ScheduledDate { get; set; }
    public string InterviewType { get; set; } = string.Empty;
    public string? Location { get; set; }
    public string? MeetingLink { get; set; }
    public string? Interviewers { get; set; }
    public string Status { get; set; } = "Scheduled";
    public string? Feedback { get; set; }
    public int? Rating { get; set; }
    public virtual Candidate? Candidate { get; set; }
}
