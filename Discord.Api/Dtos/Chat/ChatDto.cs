using Discord.Core.Entities;

namespace Discord.Api.Dtos.Chat;

public class ChatDto
{
    
    public ChatDto(
        Core.Entities.Chat chat,
        int countUnreadMessages)
    {
        Id = chat.Id;
        Name = chat.Name;
        Members = chat.Members;
        Avatar = chat.Avatar;
        Type = chat.Type;
        CountUnreadMessages = countUnreadMessages;
    }

    public string Id { get; private set; }
    public string Name { get; private set; }
    
    public List<string?> Members { get; private set; }
    
    public string Avatar { get; set; }
    
    public int Type { get; set; }
    public int CountUnreadMessages { get; set; }
    
}