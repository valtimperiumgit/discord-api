using Discord.Application.CustomValidators;
using FluentValidation;

namespace Discord.Application.User.Commands.Login;

internal class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Email).EmailFormatValidator<LoginCommand, string>();

        RuleFor(x => x.Password).NotEmpty();
    }
}