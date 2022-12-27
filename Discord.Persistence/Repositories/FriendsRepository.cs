using Discord.Core.Entities;
using Discord.Core.Repositories;
using Discord.Persistence.MongoModels;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Discord.Persistence.Repositories;

internal class FriendsRepository : IFriendsRepository
{
    private readonly IMongoClient _mongoClient;
    private readonly IMongoCollection<FriendRequestMongoModel> _friendRequestCollection;

    public FriendsRepository(IMongoClient mongoClient)
    {
        _mongoClient = mongoClient;
        _friendRequestCollection = _mongoClient
            .GetDatabase("Discord")
            .GetCollection<FriendRequestMongoModel>("FriendRequests");
    }
    
    public Task AddFriend(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task CreateFriendRequest(string requestingId, string receivingId)
    {
        var friendRequest = new FriendRequestMongoModel(
            ObjectId.GenerateNewId().ToString(),
            requestingId,
            receivingId,
            DateTime.Now
        );
        
        await _friendRequestCollection.InsertOneAsync(friendRequest);
    }

    public async Task<FriendRequest?> GetFriendRequest(string requestingId, string receivingId)
    {
        var friendRequest = (await _friendRequestCollection
                .FindAsync(request => request.RequestingId == requestingId && request.ReceivingId == receivingId))
            .FirstOrDefaultAsync();

        return (friendRequest.Result is null) ? null : friendRequest.Result.ToDomainEntity();
    }
}