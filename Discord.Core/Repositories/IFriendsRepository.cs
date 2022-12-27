using Discord.Core.Entities;

namespace Discord.Core.Repositories;

public interface IFriendsRepository
{
    Task AddFriend(string userId);

    Task CreateFriendRequest(string requestingId, string receivingId);
    Task<FriendRequest?> GetFriendRequest(string requestingId, string receivingId);
}