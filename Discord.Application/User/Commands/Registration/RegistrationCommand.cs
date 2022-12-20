using System.Windows.Input;
using Discord.Application.Abstractions.Messaging;

namespace Discord.Application.User.Commands.Registration;

public sealed record RegistrationCommand(RegistrationRequest request) : ICommand<Core.Entities.User>;