using FluentAssertions;
using FluentValidation.TestHelper;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.Modules.Employees.Commands.CreateEmployee;
using HRMS.Domain.Enums;
using Xunit;
namespace HRMS.UnitTests.Application.Employees;
public class CreateEmployeeCommandValidatorTests
{
    private ILocalizationService _localizer;

    public CreateEmployeeCommandValidatorTests(ILocalizationService localizer)
    {
        _localizer = localizer;
    }

    private readonly CreateEmployeeCommandValidator _validator;

    public CreateEmployeeCommandValidatorTests(CreateEmployeeCommandValidator validator)
    {
        _validator = validator;
    }

    [Fact]
    public void Validate_ShouldFail_WhenFirstNameIsEmpty()
    {
        var cmd = new CreateEmployeeCommand("", "Doe", null, "test@test.com", null, null, DateTime.UtcNow.AddYears(-25), Gender.Male, MaritalStatus.Single, null, null, null, null, null, DateTime.UtcNow.AddYears(-1), ContractType.Permanent, null, null, null, null, null);
        var result = _validator.TestValidate(cmd);
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
    }

    [Fact]
    public void Validate_ShouldFail_WhenEmailIsInvalid()
    {
        var cmd = new CreateEmployeeCommand("John", "Doe", null, "not-an-email", null, null, DateTime.UtcNow.AddYears(-25), Gender.Male, MaritalStatus.Single, null, null, null, null, null, DateTime.UtcNow.AddYears(-1), ContractType.Permanent, null, null, null, null, null);
        var result = _validator.TestValidate(cmd);
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void Validate_ShouldPass_WithValidData()
    {
        var cmd = new CreateEmployeeCommand("John", "Doe", null, "john.doe@company.com", null, null, DateTime.UtcNow.AddYears(-25), Gender.Male, MaritalStatus.Single, null, null, null, null, null, DateTime.UtcNow.AddYears(-1), ContractType.Permanent, null, null, null, null, null);
        var result = _validator.TestValidate(cmd);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
