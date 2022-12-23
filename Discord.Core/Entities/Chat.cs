using Discord.Core.Primitives;

namespace Discord.Core.Entities;

public class Chat : Entity
{
    public Chat(string id, string name, List<string?> members, string avatar, int type) : base(id)
    {
        Id = id;
        Name = name;
        Members = members;
        Avatar = avatar;
        Type = type;
    }

    public string Id { get; private set; }
    
    public string Name { get; private set; }
    
    public List<string?> Members { get; private set; }
    
    public string Avatar { get; set; }
    
    public int Type { get; set; }
}