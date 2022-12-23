using Discord.Application.Abstractions.Messaging;
using Discord.Application.User.Queries.GetUserByEmail;
using Discord.Core.Repositories;
using Discord.Core.Shared;

namespace Discord.Application.Chat.Queries.GetUserChats;

public sealed class GetUserChatsQueryHandler
    : IQueryHandler<GetUserChatsQuery, List<Core.Entities.Chat>>
{
    private readonly IChatRepository _chatRepository;

    public GetUserChatsQueryHandler(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }
    
    public async Task<Result<List<Core.Entities.Chat>>> Handle(
        GetUserChatsQuery request, 
        CancellationToken cancellationToken)
    {
        List<Core.Entities.Chat> chats = await _chatRepository.GetUserChats(request.userId);
        
        return chats;
    }
}