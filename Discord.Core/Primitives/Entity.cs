namespace Discord.Core.Primitives;

public class Entity
{
    public Entity(string id) => Id = id;

    protected Entity()
    {
        
    }

    public string Id { get; protected set; }
}