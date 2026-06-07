namespace HRMS.Domain.Events;
public record LeaveRequestApprovedEvent(Guid LeaveRequestId, Guid EmployeeId, string ApprovedBy, DateTime OccurredAt) { }
