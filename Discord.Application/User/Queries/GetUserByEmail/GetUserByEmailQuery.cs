using Discord.Application.Abstractions.Messaging;

namespace Discord.Application.User.Queries.GetUserByEmail;

public sealed record GetUserByEmailQuery(string email) : IQuery<Core.Entities.User>;