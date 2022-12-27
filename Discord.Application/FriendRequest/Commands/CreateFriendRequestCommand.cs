using Discord.Application.Abstractions.Messaging;

namespace Discord.Application.FriendRequest.Commands;

public sealed record CreateFriendRequestCommand(string requestingId, string receivingId) 
    : ICommand<Core.Entities.FriendRequest>;