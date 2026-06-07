namespace HRMS.Domain.Events;
public record EmployeeCreatedEvent(Guid EmployeeId, string Email, DateTime OccurredAt) { }
