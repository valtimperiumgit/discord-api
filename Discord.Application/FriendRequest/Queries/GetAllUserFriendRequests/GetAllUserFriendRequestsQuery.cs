using Discord.Application.Abstractions.Messaging;

namespace Discord.Application.FriendRequest.Queries.GetAllUserFriendRequests;

public sealed record GetAllUserFriendRequestsQuery(string userId) 
    : IQuery<List<Core.Entities.FriendRequest>>;