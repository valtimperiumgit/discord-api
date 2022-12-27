using Discord.Core.Primitives;

namespace Discord.Core.Entities;

public class FriendRequest : Entity
{
    public FriendRequest(string id, string requestingId, string receivingId, DateTime create) : base(id)
    {
        RequestingId = requestingId;
        ReceivingId = receivingId;
        Create = create;
    }

    protected FriendRequest(string requestingId, string receivingId, DateTime create)
    {
        RequestingId = requestingId;
        ReceivingId = receivingId;
        Create = create;
    }

    public string RequestingId { get; private set; }
    public string ReceivingId { get; private set; }
    public DateTime Create { get; private set; }
}