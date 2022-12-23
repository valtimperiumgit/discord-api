using Discord.Core.Entities;
using Discord.Core.Repositories;
using Discord.Persistence.MongoModels;
using MongoDB.Driver;

namespace Discord.Persistence.Repositories;

public class ChatRepository : IChatRepository
{    
    private readonly IMongoClient _mongoClient;
    private readonly IMongoCollection<ChatMongoModel> _chatsCollection;

    public ChatRepository(IMongoClient mongoClient)
    {
        _mongoClient = mongoClient;
        _chatsCollection = _mongoClient
            .GetDatabase("Discord")
            .GetCollection<ChatMongoModel>("Chats");
    }
    public async Task<List<Chat>> GetUserChats(string? userId)
    {
        var chatsModels = (await _chatsCollection
            .FindAsync(chat => chat.Members.Contains(userId))).ToList();

        var chats = chatsModels.Select(chat => chat.ToDomainEntity()).ToList();

        return chats;
    }
}