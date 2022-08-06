using Membership.Core.ValueObjects;
namespace Membership.Core.Entities.Nationalities;

public class State 
{
    public Guid Id { get; private set; }
    public StateName Name { get; private set; }
    public string Prefix { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private State(Guid id, StateName name, string prefix, bool isDeleted, DateTime createdAt)
    {
        Id = id;
        Name = name;
        Prefix = prefix;
        IsDeleted = isDeleted;
        CreatedAt = createdAt;
    }

    private State()
    {
    }

    public static State Create(Guid id, StateName name, string prefix, DateTime createdAt)
        => new(id, name, prefix, false, createdAt);
    
    public void Update(StateName name)
    {
        Name = name;
    }
    
    public void Delete()
    {
        IsDeleted = true;
    }
}
