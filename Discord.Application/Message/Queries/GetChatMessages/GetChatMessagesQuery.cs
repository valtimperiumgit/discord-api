using Discord.Application.Abstractions.Messaging;

namespace Discord.Application.Message.Queries.GetChatMessages;

public sealed record GetChatMessagesQuery(string chatId, int take, int skip) 
    : IQuery<List<Core.Entities.Message>>;