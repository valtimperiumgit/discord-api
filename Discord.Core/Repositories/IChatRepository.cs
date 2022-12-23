using Discord.Core.Entities;

namespace Discord.Core.Repositories;

public interface IChatRepository
{
    Task<List<Chat>> GetUserChats(string? userId);
}