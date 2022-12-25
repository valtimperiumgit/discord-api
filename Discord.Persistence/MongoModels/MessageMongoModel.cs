using Discord.Core.Entities;
using Discord.Persistence.Primitives;
using MongoDB.Bson.Serialization.Attributes;

namespace Discord.Persistence.MongoModels;

public class MessageMongoModel : MongoModel<Message>
{
    public MessageMongoModel(string id,
        string userId,
        string content,
        int type,
        DateTime created,
        DateTime edited,
        List<string> readers,
        string chatId) 
        : base(id)
    {
        UserId = userId;
        Content = content;
        Type = type;
        Created = created;
        Edited = edited;
        Readers = readers;
        ChatId = chatId;
    }

    [BsonElement("userId")] 
    public string UserId { get; private set; }
    [BsonElement("content")] 
    public string Content { get; private set; }
    [BsonElement("chatId")] 
    public string ChatId { get; private set; }
    [BsonElement("type")] 
    public int Type { get; private set; }
    [BsonElement("created")] 
    public DateTime Created { get; private set; }
    [BsonElement("edited")] 
    public DateTime Edited { get; private set; }
    [BsonElement("readers")] 
    public List<string> Readers { get; private set; }
    
    public Message ToDomainEntity()
    {
        return new Message(
            Id,
            UserId,
            Content,
            Type,
            Created,
            Edited,
            Readers,
            ChatId);
    }
}