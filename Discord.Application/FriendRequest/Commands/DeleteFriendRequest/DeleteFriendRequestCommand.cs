using Discord.Application.Abstractions.Messaging;

namespace Discord.Application.FriendRequest.Commands.DeleteFriendRequest;

public sealed record DeleteFriendRequestCommand(string userId, string requestId) : ICommand;