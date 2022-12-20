using Discord.Core.Entities;
using Discord.Persistence.Primitives;
using MongoDB.Bson.Serialization.Attributes;

namespace Discord.Persistence.MongoModels;

public class UserMongoModel : MongoModel<User>
{
    public UserMongoModel(
        string id,
        string email,
        string password,
        string name,
        string tag,
        string? avatar,
        DateTime created,
        BirthdayMongoModel birthday,
        bool isAcceptNewsletters)
        : base(id)
    {
        Email = email;
        Password = password;
        Name = name;
        Tag = tag;
        Avatar = avatar;
        Created = created;
        Birthday = birthday;
        IsAcceptNewsletters = isAcceptNewsletters;
    }

    [BsonElement("email")]
    public string Email { get; set; }
    [BsonElement("password")] 
    public string Password { get; set; }
    [BsonElement("name")] 
    public string Name { get; set; }
    [BsonElement("tag")] 
    public string Tag { get; set; }
    [BsonElement("avatar")] 
    public string? Avatar { get; set; }
    [BsonElement("created")]
    public DateTime Created { get; set; }

    
    [BsonElement("birthday")] 
    public BirthdayMongoModel Birthday { get; set; }
    [BsonElement("isAcceptNewsletters")]
    public bool IsAcceptNewsletters { get; set; }

    public User ToDomainEntity()
    {
        return new User(
            Id,
            Name,
            Core.ValueObject.Email
                .Create(Email).Value,
            Core.ValueObject.Password
                .Create(Password).Value,
            Tag,
            Avatar,
            Created,
            Core.ValueObject.Birthday
                .Create(
                    Birthday.Year,
                    Birthday.Month,
                    Birthday.Day)
                .Value,
            IsAcceptNewsletters
        );
    }
}