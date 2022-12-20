using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Discord.Persistence.Primitives;

public abstract class MongoModel<TEntity>
{
    public MongoModel(string id)
    {
        Id = id;
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
}