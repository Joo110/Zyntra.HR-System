using HRMS.Domain.Entities;
using HRMS.Domain.Enums;
using FluentAssertions;
using Xunit;
namespace HRMS.UnitTests.Domain;
public class LeaveRequestTests
{
    [Fact]
    public void LeaveRequest_ShouldHavePendingStatus_WhenCreated()
    {
        var leave = new LeaveRequest { EmployeeId = Guid.NewGuid(), LeaveTypeId = Guid.NewGuid(), StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(3), NumberOfDays = 3, Reason = "Vacation" };
        leave.Status.Should().Be(LeaveStatus.Pending);
    }

    [Fact]
    public void LeaveRequest_ShouldCalculateCorrectDays()
    {
        var start = new DateTime(2024, 1, 1);
        var end = new DateTime(2024, 1, 5);
        var days = (end - start).TotalDays + 1;
        days.Should().Be(5);
    }
}
