using Discord.Application.Abstractions.Messaging;

namespace Discord.Application.Chat.Queries.GetUserChats;

public sealed record GetUserChatsQuery(string? userId) : IQuery<List<Core.Entities.Chat>>;