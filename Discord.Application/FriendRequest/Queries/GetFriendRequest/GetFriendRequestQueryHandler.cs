using Discord.Application.Abstractions.Messaging;
using Discord.Core.Errors;
using Discord.Core.Repositories;
using Discord.Core.Shared;

namespace Discord.Application.FriendRequest.Queries.GetFriendRequest;

public class GetFriendRequestQueryHandler
    : IQueryHandler<GetFriendRequestQuery, Core.Entities.FriendRequest>
{
    private readonly IFriendsRepository _friendsRepository;

    public GetFriendRequestQueryHandler(
        IFriendsRepository friendsRepository)
    {
        _friendsRepository = friendsRepository;
    }

    public async Task<Result<Core.Entities.FriendRequest>> Handle(
        GetFriendRequestQuery request,
        CancellationToken cancellationToken)
    {
       var friendRequest = await _friendsRepository.GetFriendRequest(request.requestingId, request.receivingId);

       if (friendRequest is null)
       {
           return Result.Failure<Core.Entities.FriendRequest>(DomainErrors.FriendRequest.RequestNotFound);
       }

       return friendRequest;
    }
}