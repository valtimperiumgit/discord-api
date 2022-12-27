using Discord.Application.Abstractions.Messaging;

namespace Discord.Application.User.Queries.GetUserById;

public sealed record GetUserByIdQuery(string id) : IQuery<Core.Entities.User>;