using Membership.Core.ValueObjects;

namespace Membership.Core.Entities.Nationalities;

public class Area
{
    public Guid Id { get; private set; }
    public AreaName Name{ get; private set; }
    public Guid StateId { get; private set; }
    public State State { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Area(Guid id, AreaName name, Guid stateId, bool isDeleted, DateTime createdAt)
    {
        Id = id;
        Name = name;
        StateId = stateId;
        IsDeleted = isDeleted;
        CreatedAt = createdAt;
    }

    private Area()
    {
    }

    public static Area Create(Guid id, AreaName name, Guid stateId, DateTime createdAt)
        => new(id, name, stateId, false, createdAt);

    
    public void Update(AreaName name, Guid stateId)
    {
        Name = name;
        StateId = stateId;
    }
    
    public void Delete()
    {
        IsDeleted = true;
    }
}