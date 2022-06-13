using Membership.Core.ValueObjects;

namespace Membership.Core.Entities.Nationalities;

public class District
{
    public GenericId Id { get; private set; }
    public DistrictName Name{ get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private District(GenericId id, DistrictName name, bool isDeleted, DateTime createdAt)
    {
        Id = id;
        Name = name;
        IsDeleted = isDeleted;
        CreatedAt = createdAt;
    }

    public District()
    {
    }

    public static District Create(GenericId id, DistrictName name, DateTime createdAt)
        => new(id, name, false, createdAt);

    public void Update(DistrictName name)
    {
        Name = name;
    }
    
    public void  Delete()
    {
        IsDeleted = true;
    }
}