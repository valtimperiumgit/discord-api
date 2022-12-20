using Discord.Application.Abstractions.Messaging;

namespace Discord.Application.User.Commands.Login;

public sealed record LoginCommand(string Email, string Password) : ICommand<string>;