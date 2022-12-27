using FluentValidation;

namespace Discord.Application.User.Queries.GetUserByNameAndTag;

public class GetUserByNameAndTagValidator : AbstractValidator<GetUserByNameAndTagQuery>
{
    public GetUserByNameAndTagValidator()
    {
        RuleFor(x => x.name).NotEmpty();
        
        RuleFor(x => x.tag).NotEmpty();
    }
}