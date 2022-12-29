using Discord.Application.Abstractions.Messaging;
using Discord.Application.User.Commands.DeleteFriend;
using Discord.Core.Errors;
using Discord.Core.Repositories;
using Discord.Core.Shared;

namespace Discord.Application.FriendRequest.Commands.DeleteFriendRequest;

public class DeleteFriendRequestCommandHandler
    : ICommandHandler<DeleteFriendRequestCommand>

{
    private readonly IFriendsRepository _friendsRepository;
    private readonly IUserRepository _userRepository;
    
    public DeleteFriendRequestCommandHandler(
        IFriendsRepository friendsRepository,
        IUserRepository userRepository)
    {
        _friendsRepository = friendsRepository;
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(
        DeleteFriendRequestCommand request,
        CancellationToken cancellationToken)
    {
        var friendRequest = await _friendsRepository.GetFriendRequestById(request.requestId);

        if (friendRequest is null)
        {
            return Result.Failure(DomainErrors.FriendRequest.RequestNotFound);
        }

        if ((friendRequest.RequestingId != request.userId) && (friendRequest.ReceivingId != request.userId))
        {
            return Result.Failure(DomainErrors.FriendRequest.UserNotHavePermission);
        }

        await _friendsRepository.DeleteFriendRequest(request.requestId);
        
        return Result.Success();
    }
}