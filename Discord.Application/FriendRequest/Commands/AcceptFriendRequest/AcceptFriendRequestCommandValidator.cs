using Discord.Application.FriendRequest.Commands.CreateFriendsRequest;
using FluentValidation;

namespace Discord.Application.FriendRequest.Commands.AcceptFriendRequest;

public class AcceptFriendRequestCommandValidator : AbstractValidator<AcceptFriendRequestCommand>
{
    public AcceptFriendRequestCommandValidator()
    {
        RuleFor(x => x.friendRequestId).NotEmpty();
    }
}