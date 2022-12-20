using Discord.Application.CustomValidators;
using Discord.Application.User.Commands.Login;
using FluentValidation;

namespace Discord.Application.User.Commands.Registration;

internal class RegistrationCommandValidator : AbstractValidator<RegistrationCommand>
{
    public RegistrationCommandValidator()
    {
        RuleFor(x => x.request.Email).NotEmpty();
        RuleFor(x => x.request.Email).EmailFormatValidator<RegistrationCommand, string>();

        RuleFor(x => x.request.Password).NotEmpty();
        
        RuleFor(x => x.request.Name).NotEmpty();
    }
}