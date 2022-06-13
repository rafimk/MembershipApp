using Membership.Core.ValueObjects;

namespace Membership.Core.Entities.Nationalities;

public class Mandalam
{
    public GenericId Id { get; private set; }
    public MandalamName Name{ get; private set; }
    public GenericId DistrictId { get; private set; }
    public District District{ get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Mandalam(GenericId id, MandalamName name, GenericId districtId, bool isDeleted, DateTime createdAt)
    {
        Id = id;
        Name = name;
        DistrictId = districtId;
        IsDeleted = isDeleted;
        CreatedAt = createdAt;
    }

    private Mandalam()
    {
    }

    public static Mandalam Create(GenericId id, MandalamName name, GenericId districtId, DateTime createdAt)
        => new(id, name, districtId, false, createdAt);

    public void Update(MandalamName name, GenericId districtId)
    {
        Name = name;
        DistrictId = districtId;
    }
    
    public void  Delete()
    {
        IsDeleted = true;
    }
}