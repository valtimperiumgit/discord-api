using Discord.Application.Abstractions.Messaging;
using Discord.Core.Repositories;
using Discord.Core.Shared;

namespace Discord.Application.FriendRequest.Queries.GetFriendRequestById;

public class GetFriendRequestQueryHandler 
    : IQueryHandler<GetFriendRequestByIdQuery, Core.Entities.FriendRequest>
{
    private readonly IFriendsRepository _friendsRepository;

    public GetFriendRequestQueryHandler(
        IFriendsRepository friendsRepository)
    {
        _friendsRepository = friendsRepository;
    }
    
    public async Task<Result<Core.Entities.FriendRequest>> Handle(
        GetFriendRequestByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _friendsRepository.GetFriendRequestById(request.id);
    }
}