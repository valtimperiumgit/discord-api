using Discord.Application.Abstractions.Messaging;

namespace Discord.Application.User.Queries.GetCurrentUsers;

public sealed record GetCurrentUsersQuery(string userId) : IQuery<List<Core.Entities.User>>;
