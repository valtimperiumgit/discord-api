using Discord.Application.CustomValidators;
using FluentValidation;
namespace Discord.Application.FriendRequest.Commands;

public class CreateFriendRequestCommandValidator : AbstractValidator<CreateFriendRequestCommand>
{
    public CreateFriendRequestCommandValidator()
    {
        RuleFor(x => x.receivingId).NotEmpty();
        RuleFor(x => x.requestingId).NotEmpty();
    }
}