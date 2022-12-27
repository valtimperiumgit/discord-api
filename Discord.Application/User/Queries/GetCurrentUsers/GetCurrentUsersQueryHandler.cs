using Discord.Application.Abstractions.Messaging;
using Discord.Core.Repositories;
using Discord.Core.Shared;

namespace Discord.Application.User.Queries.GetCurrentUsers;

public sealed class GetCurrentUsersQueryHandler
    : IQueryHandler<GetCurrentUsersQuery, List<Core.Entities.User>>
{
    
    private readonly IUserRepository _userRepository;
    private readonly IChatRepository _chatRepository;
    
    public GetCurrentUsersQueryHandler(
        IUserRepository userRepository,
        IChatRepository chatRepository)
    {
        _userRepository = userRepository;
        _chatRepository = chatRepository;
    }
    
    public async Task<Result<List<Core.Entities.User>>> Handle(
        GetCurrentUsersQuery request,
        CancellationToken cancellationToken)
    {
        var currentUsers = new List<Core.Entities.User>();

        var chats = await _chatRepository.GetUserChats(request.userId);
        var chatsMembersIds = new List<string>();

        foreach (var chat in chats)
        {
            chatsMembersIds.AddRange(chat.Members);
        }

        var chatsUsers = await _userRepository
            .GetUsersByIds(chatsMembersIds
                .FindAll(memberId => memberId != request.userId)
                .Distinct()
                .ToList());
        
        //friends..
        
        currentUsers.AddRange(
            chatsUsers.Distinct());
        
        return currentUsers;
    }
}