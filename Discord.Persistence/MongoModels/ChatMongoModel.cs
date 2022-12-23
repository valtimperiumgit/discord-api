using Discord.Core.Entities;
using Discord.Persistence.Primitives;
using MongoDB.Bson.Serialization.Attributes;

namespace Discord.Persistence.MongoModels;

public class ChatMongoModel : MongoModel<Chat>
{
    public ChatMongoModel(string id, string name, List<string?> members, string avatar) : base(id)
    {
        Name = name;
        Members = members;
        Avatar = avatar;
    }

    [BsonElement("name")] 
    public string Name { get; private set; }
    
    [BsonElement("members")] 
    public List<string?> Members { get; private set; }
    
    [BsonElement("avatar")] 
    public string Avatar { get; private set; }
    
    [BsonElement("type")] 
    public int Type { get; private set; }

    public Chat ToDomainEntity()
    {
        return new Chat(
            Id,
            Name,
            Members,
            Avatar,
            Type);
    }
}