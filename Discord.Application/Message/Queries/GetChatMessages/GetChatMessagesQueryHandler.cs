using Discord.Application.Abstractions.Messaging;
using Discord.Core.Shared;

namespace Discord.Application.Message.Queries.GetChatMessages;

public class GetChatMessagesQueryHandler
    : IQueryHandler<GetChatMessagesQuery, List<Core.Entities.Message>>
{
    public Task<Result<List<Core.Entities.Message>>> Handle(
        GetChatMessagesQuery request, 
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}