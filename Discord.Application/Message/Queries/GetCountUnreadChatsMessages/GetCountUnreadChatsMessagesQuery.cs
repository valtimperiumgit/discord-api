using Discord.Application.Abstractions.Messaging;

namespace Discord.Application.Message.Queries.GetCountUnreadChatsMessages;

public sealed record GetUnreadChatsMessagesQuery(List<string> chatsIds, string userId) 
    : IQuery<List<Core.Entities.Message>>;