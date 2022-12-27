using Discord.Application.Abstractions.Messaging;

namespace Discord.Application.User.Queries.GetUserByNameAndTag;

public sealed record GetUserByNameAndTagQuery(string name, string tag) : IQuery<Core.Entities.User>;