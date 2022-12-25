using Discord.Application.Abstractions.Messaging;
using Discord.Core.Repositories;
using Discord.Core.Shared;

namespace Discord.Application.Message.Queries.GetCountUnreadChatsMessages;

public class GetCountUnreadChatMessagesQueryHandler :
    IQueryHandler<GetUnreadChatsMessagesQuery, List<Core.Entities.Message>>
{
    private readonly IMessageRepository _messageRepository;

    public GetCountUnreadChatMessagesQueryHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }
    
    public async Task<Result<List<Core.Entities.Message>>> Handle(
        GetUnreadChatsMessagesQuery request, 
        CancellationToken cancellationToken)
    {
        List<Core.Entities.Message> messages = await _messageRepository
            .GetUnreadChatsMessages(request.chatsIds, request.userId);

        return messages;
    }
}