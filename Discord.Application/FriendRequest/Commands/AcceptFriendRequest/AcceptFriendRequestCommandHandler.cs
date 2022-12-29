using Discord.Application.Abstractions.Messaging;
using Discord.Core.Errors;
using Discord.Core.Repositories;
using Discord.Core.Shared;

namespace Discord.Application.FriendRequest.Commands.AcceptFriendRequest;

public class AcceptFriendRequestCommandHandler
    : ICommandHandler<AcceptFriendRequestCommand>
{
    private readonly IFriendsRepository _friendsRepository;
    private readonly IUserRepository _userRepository;
    
    public AcceptFriendRequestCommandHandler(
        IFriendsRepository friendsRepository,
        IUserRepository userRepository)
    {
        _friendsRepository = friendsRepository;
        _userRepository = userRepository;
    }
    
    public async Task<Result> Handle(
        AcceptFriendRequestCommand request,
        CancellationToken cancellationToken)
    {
        var friendRequest = await _friendsRepository.GetFriendRequestById(request.friendRequestId);

        if (friendRequest is null)
        {
            return Result.Failure<Core.Entities.FriendRequest>(DomainErrors.FriendRequest.RequestNotFound);
        }

        if (friendRequest.ReceivingId != request.userId)
        {
            return Result.Failure<Core.Entities.FriendRequest>(DomainErrors.FriendRequest.AcceptNotYourRequest);
        }

        await _userRepository.AddFriend(friendRequest.RequestingId, friendRequest.ReceivingId);

        await _friendsRepository.DeleteFriendRequestById(friendRequest.Id);

        return Result.Success();
    }
}