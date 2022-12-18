using Discord.Core.Primitives;

namespace Discord.Core.Entities;

public class User : Entity
{
    public User(string id, string name, string email, string password, string tag, string avatar, DateTime created) 
        : base(id)
    {
        Name = name;
        Email = email;
        Password = password;
        Tag = tag;
        Avatar = avatar;
        Created = created;
    }

    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string Tag { get; private set; }
    public string Avatar { get; private set; }
    public DateTime Created { get; set; }
}