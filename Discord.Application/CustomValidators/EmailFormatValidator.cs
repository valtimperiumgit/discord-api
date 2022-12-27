using FluentValidation;

namespace Discord.Application.CustomValidators;

public static class CustomValidators {
    public static IRuleBuilderOptions<T, string> 
        EmailFormatValidator<T, TElement>(this IRuleBuilder<T, string> ruleBuilder) {
        return ruleBuilder.Must(email => email.Split('@').Length == 2)
            .WithMessage("InvalidEmailFormat");
    }
    
    public static IRuleBuilderOptions<T, (string,string)> 
        StringsNotEquel<T, TElement>(this IRuleBuilder<T, (string,string)> ruleBuilder) {
        return ruleBuilder.Must(values => values.Item1 != values.Item2)
            .WithMessage("InvalidEmailFormat");
    }
}