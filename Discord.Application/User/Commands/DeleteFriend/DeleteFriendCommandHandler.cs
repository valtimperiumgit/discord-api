using Discord.Application.Abstractions.Messaging;
using Discord.Core.Errors;
using Discord.Core.Repositories;
using Discord.Core.Shared;

namespace Discord.Application.User.Commands.DeleteFriend;

public class DeleteFriendCommandHandler :
    ICommandHandler<DeleteFriendCommand>
{
    private readonly IUserRepository _userRepository;
    
    public DeleteFriendCommandHandler(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<Result> Handle(
        DeleteFriendCommand request,
        CancellationToken cancellationToken)
    {
        var friend = await _userRepository.GetUserById(request.friendId);

        if (friend is null)
        {
            return Result.Failure(DomainErrors.User.UserNotFound);
        }

        if (!friend.Friends.Contains(request.userId))
        {
            return Result.Failure(DomainErrors.Friend.UsersAreNotFriends);
        }

        await _userRepository.DeleteFriend(request.userId, request.friendId);
        
        return Result.Success();
    }
}