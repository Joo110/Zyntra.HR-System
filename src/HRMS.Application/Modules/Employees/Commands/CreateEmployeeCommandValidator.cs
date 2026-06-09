using FluentValidation;
using HRMS.Application.Common.Interfaces;
namespace HRMS.Application.Modules.Employees.Commands.CreateEmployee;
public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator(ILocalizationService _localizer)
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(320);
        RuleFor(x => x.DateOfBirth).NotEmpty().LessThan(DateTime.UtcNow.AddYears(-18)).WithMessage(_localizer["Employeemustbeatleast18yearsold"]);
        RuleFor(x => x.HireDate).NotEmpty().LessThanOrEqualTo(DateTime.UtcNow);
        RuleFor(x => x.BasicSalary).GreaterThan(0).When(x => x.BasicSalary.HasValue);
    }
}
