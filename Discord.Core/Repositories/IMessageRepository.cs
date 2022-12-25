using Discord.Core.Entities;

namespace Discord.Core.Repositories;

public interface IMessageRepository
{
    public Task<List<Message>> GetUnreadChatsMessages(List<string> chatsIds, string userId);
}