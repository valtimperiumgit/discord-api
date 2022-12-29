using Discord.Application.Abstractions.Messaging;

namespace Discord.Application.FriendRequest.Commands.AcceptFriendRequest;

public sealed record AcceptFriendRequestCommand(string userId, string friendRequestId) : ICommand;