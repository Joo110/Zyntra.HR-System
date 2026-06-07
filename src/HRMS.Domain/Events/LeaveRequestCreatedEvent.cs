namespace HRMS.Domain.Events;
public record LeaveRequestCreatedEvent(Guid LeaveRequestId, Guid EmployeeId, DateTime OccurredAt) { }
