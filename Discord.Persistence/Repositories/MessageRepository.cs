using Discord.Core.Entities;
using Discord.Core.Repositories;
using Discord.Persistence.MongoModels;
using MongoDB.Driver;

namespace Discord.Persistence.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly IMongoClient _mongoClient;
    private readonly IMongoCollection<MessageMongoModel> _messageCollection;

    public MessageRepository(IMongoClient mongoClient)
    {
        _mongoClient = mongoClient;
        _messageCollection = _mongoClient
            .GetDatabase("Discord")
            .GetCollection<MessageMongoModel>("Messages");
    }
    public async Task<List<Message>> GetUnreadChatsMessages(List<string> chatsIds, string userId)
    {
        var messages = (await _messageCollection
            .FindAsync(message => chatsIds.Contains(message.ChatId) 
            && message.UserId != userId
            && !message.Readers.Contains(userId))).ToList();

        return messages.Select(message => message.ToDomainEntity()).ToList();
    }
}