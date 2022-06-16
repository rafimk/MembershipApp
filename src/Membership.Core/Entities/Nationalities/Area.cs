using Membership.Core.ValueObjects;

namespace Membership.Core.Entities.Nationalities;

public class Area
{
    public GenericId Id { get; private set; }
    public AreaName Name{ get; private set; }
    public GenericId StateId { get; private set; }
    public State State { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Area(GenericId id, AreaName name, GenericId stateId, bool isDeleted, DateTime createdAt)
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

    public static Area Create(GenericId id, AreaName name, GenericId stateId, DateTime createdAt)
        => new(id, name, stateId, false, createdAt);

    
    public void Update(AreaName name, GenericId stateId)
    {
        Name = name;
        StateId = stateId;
    }
    
    public void Delete()
    {
        IsDeleted = true;
    }
}