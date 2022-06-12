using Membership.Core.ValueObjects;
namespace Membership.Core.Entities.Nationalities;

public class State 
{
    public GenericId Id { get; private set; }
    public StateName Name { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private State(GenericId id, StateName name, bool isDeleted, DateTime createdAt)
    {
        Id = id;
        Name = name;
        IsDeleted = isDeleted;
        CreatedAt = createdAt;
    }

    private State()
    {
    }

    public static State Create(GenericId id, StateName name, DateTime createdAt)
        => new(id, name, false, createdAt);
    
    public void Update(StateName name)
    {
        Name = name;
    }
    
    public void Delete()
    {
        IsDeleted = true;
    }
}
