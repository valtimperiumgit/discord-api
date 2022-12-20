using FluentValidation;

namespace Discord.Application.CustomValidators;

public static class CustomValidators {
    public static IRuleBuilderOptions<T, string> 
        EmailFormatValidator<T, TElement>(this IRuleBuilder<T, string> ruleBuilder) {
        return ruleBuilder.Must(email => email.Split('@').Length == 2)
            .WithMessage("InvalidEmailFormat");
    }
}