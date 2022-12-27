using Discord.Core.Entities;
using Discord.Persistence.Primitives;
using MongoDB.Bson.Serialization.Attributes;

namespace Discord.Persistence.MongoModels;

public class FriendRequestMongoModel : MongoModel<FriendRequest>
{
    public FriendRequestMongoModel(
        string id, 
        string requestingId,
        string receivingId,
        DateTime create) 
        : base(id)
    {
        RequestingId = requestingId;
        ReceivingId = receivingId;
        Create = create;
    }

    [BsonElement("requestingId")] 
    public string RequestingId { get; private set; }
    [BsonElement("receivingId")] 
    public string ReceivingId { get; private set; }
    [BsonElement("create")] 
    public DateTime Create { get; private set; }
    
    public FriendRequest ToDomainEntity()
    {
        return new FriendRequest(
          Id,
          RequestingId,
          ReceivingId,
          Create
        );
    }
}