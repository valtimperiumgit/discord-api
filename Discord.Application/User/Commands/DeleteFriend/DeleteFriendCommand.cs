using Discord.Application.Abstractions.Messaging;

namespace Discord.Application.User.Commands.DeleteFriend;

public sealed record DeleteFriendCommand(string userId, string friendId) : ICommand;