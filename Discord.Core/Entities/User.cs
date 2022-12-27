using Discord.Core.Primitives;
using Discord.Core.ValueObject;

namespace Discord.Core.Entities;

public class User : Entity
{
    public User(
        string id,
        string name,
        Email email, 
        Password password,
        string tag,
        string avatar,
        DateTime created,
        Birthday birthday,
        bool isAcceptNewsletters) 
        : base(id)
    {
        Name = name;
        Email = email;
        Password = password;
        Tag = tag;
        Avatar = avatar;
        Created = created;
        Birthday = birthday;
        IsAcceptNewsletters = isAcceptNewsletters;
    }

    public string Name { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public string Tag { get; private set; }
    public string Avatar { get; private set; }
    public DateTime Created { get; private set; }
    
    public Birthday Birthday { get; private set; }
    
    public bool IsAcceptNewsletters { get; private set; }
    public List<string> Friends { get; private set; }
}