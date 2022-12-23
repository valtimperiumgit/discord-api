using Discord.Core.Primitives;

namespace Discord.Core.Entities;

public class Message : Entity
{
    public Message(string id, string userId, string content, int type, DateTime created, DateTime edited, List<string> readers) : base(id)
    {
        Id = id;
        UserId = userId;
        Content = content;
        Type = type;
        Created = created;
        Edited = edited;
        Readers = readers;
    }

    public string Id { get; private set; }
    
    public string UserId { get; private set; }
    
    public string Content { get; private set; }
    
    public int Type { get; private set; }
    
    public DateTime Created { get; private set; }
    
    public DateTime Edited { get; private set; }
    
    public List<string> Readers { get; private set; }
}