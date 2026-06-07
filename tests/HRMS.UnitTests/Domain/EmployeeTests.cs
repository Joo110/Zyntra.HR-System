using HRMS.Domain.Entities;
using HRMS.Domain.Enums;
using FluentAssertions;
using Xunit;
namespace HRMS.UnitTests.Domain;
public class EmployeeTests
{
    [Fact]
    public void Employee_ShouldBeCreated_WithValidData()
    {
        var employee = new Employee
        {
            FirstName = "John", LastName = "Doe", Email = "john.doe@example.com",
            EmployeeCode = "EMP001", HireDate = DateTime.UtcNow, Status = EmployeeStatus.Active
        };
        employee.Should().NotBeNull();
        employee.Id.Should().NotBeEmpty();
        employee.FirstName.Should().Be("John");
        employee.Status.Should().Be(EmployeeStatus.Active);
        employee.IsDeleted.Should().BeFalse();
    }

    [Fact]
    public void Employee_ShouldHaveDefaultStatus_WhenCreated()
    {
        var employee = new Employee { FirstName = "Jane", LastName = "Doe", Email = "jane@example.com", EmployeeCode = "EMP002", HireDate = DateTime.UtcNow };
        employee.Status.Should().Be(EmployeeStatus.Active);
    }

    [Fact]
    public void Employee_ShouldBeMarkedAsDeleted_WhenSoftDeleted()
    {
        var employee = new Employee { FirstName = "Test", LastName = "User", Email = "test@example.com", EmployeeCode = "EMP003", HireDate = DateTime.UtcNow };
        employee.IsDeleted = true;
        employee.DeletedAt = DateTime.UtcNow;
        employee.IsDeleted.Should().BeTrue();
        employee.DeletedAt.Should().NotBeNull();
    }
}
