

namespace Discord.Application.Hubs;

public interface IFriendsHub 
{
    Task DeleteFriend(string id);
    
    Task AddFriend(string id);
}