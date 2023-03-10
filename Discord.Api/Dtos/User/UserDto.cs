using Discord.Core.ValueObject;

namespace Discord.Api.Dtos.User;

public class UserDto
{
    public UserDto(Core.Entities.User user)
    {
        Id = user.Id;
        Name = user.Name;
        Email = user.Email;
        Tag = user.Tag;
        Avatar = user.Avatar;
        Created = user.Created;
        Birthday = user.Birthday;
        IsAcceptNewsletters = user.IsAcceptNewsletters;
        Friends = user.Friends;
    }

    public string Id { get; private set; }
    public string Name { get; private set; }
    public Email Email { get; private set; }
    public string Tag { get; private set; }
    public string Avatar { get; private set; }
    public DateTime Created { get; private set; }
    public Birthday Birthday { get; private set; }
    public bool IsAcceptNewsletters { get; private set; }
    
    public List<string> Friends { get; private set; }
}