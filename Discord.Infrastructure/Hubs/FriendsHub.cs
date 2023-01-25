using Discord.Application.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Discord.Infrastructure.Hubs;

public class FriendsHub : Hub, IFriendsHub
{
    public async Task DeleteFriend(string id)
    {
        throw new NotImplementedException();
    }

    public async Task AddFriend(string id)
    {
        await Clients.All.SendAsync("AddFriend", "test" + id);
    }
}