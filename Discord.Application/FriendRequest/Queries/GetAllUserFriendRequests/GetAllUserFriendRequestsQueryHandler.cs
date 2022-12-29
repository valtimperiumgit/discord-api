using Discord.Application.Abstractions.Messaging;
using Discord.Core.Repositories;
using Discord.Core.Shared;

namespace Discord.Application.FriendRequest.Queries.GetAllUserFriendRequests;

public class GetAllUserFriendRequestsQueryHandler :
    IQueryHandler<GetAllUserFriendRequestsQuery, List<Core.Entities.FriendRequest>>
{
    private readonly IFriendsRepository _friendsRepository;

    public GetAllUserFriendRequestsQueryHandler(
        IFriendsRepository friendsRepository)
    {
        _friendsRepository = friendsRepository;
    }
    
    public async Task<Result<List<Core.Entities.FriendRequest>>> Handle(
        GetAllUserFriendRequestsQuery request,
        CancellationToken cancellationToken)
    {
        return await _friendsRepository.GetAllUserFriendRequests(request.userId);
    }
}