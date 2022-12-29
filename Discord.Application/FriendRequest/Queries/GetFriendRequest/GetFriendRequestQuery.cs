using Discord.Application.Abstractions.Messaging;

namespace Discord.Application.FriendRequest.Queries.GetFriendRequest;

public sealed record GetFriendRequestQuery(string requestingId, string receivingId) 
    : IQuery<Core.Entities.FriendRequest>;