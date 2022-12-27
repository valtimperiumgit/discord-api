using FluentValidation;

namespace Discord.Application.FriendRequest.Commands.CreateFriendsRequest;

public class CreateFriendRequestCommandValidator : AbstractValidator<CreateFriendRequestCommand>
{
    public CreateFriendRequestCommandValidator()
    {
        RuleFor(x => x.receivingId).NotEmpty();
        RuleFor(x => x.requestingId).NotEmpty();
    }
}