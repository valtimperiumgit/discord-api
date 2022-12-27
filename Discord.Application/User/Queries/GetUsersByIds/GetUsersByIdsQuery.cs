using Discord.Application.Abstractions.Messaging;

namespace Discord.Application.User.Queries.GetUsersByIds;

public sealed record GetUsersByIdsQuery(List<string> ids) : IQuery<List<Core.Entities.User>>;