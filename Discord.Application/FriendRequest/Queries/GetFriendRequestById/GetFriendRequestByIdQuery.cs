using Discord.Application.Abstractions.Messaging;

namespace Discord.Application.FriendRequest.Queries.GetFriendRequestById;

public sealed record GetFriendRequestByIdQuery(string id) : IQuery<Core.Entities.FriendRequest>;