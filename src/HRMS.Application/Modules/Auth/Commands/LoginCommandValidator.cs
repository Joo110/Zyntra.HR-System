using FluentValidation;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.Common.Localization;
using Microsoft.Extensions.Localization;
namespace HRMS.Application.Modules.Auth.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator(
        ILocalizationService localizer)
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(localizer["EmailRequired"])
            .EmailAddress()
            .WithMessage(localizer["ValidEmailRequired"]);

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(localizer["PasswordRequired"])
            .MinimumLength(6)
            .WithMessage(localizer["PasswordMinLength"]);
    }
}
