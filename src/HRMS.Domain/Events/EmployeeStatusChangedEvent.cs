namespace HRMS.Domain.Events;
public record EmployeeStatusChangedEvent(Guid EmployeeId, string OldStatus, string NewStatus, DateTime OccurredAt) { }
