using Membership.Core.ValueObjects;

namespace Membership.Core.Entities.Memberships.WelfareSchemes;

public class WelfareScheme 
{
    public GenericId Id { get; private set; }
    public WelfareSchemeName Name{ get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private WelfareScheme(GenericId id, WelfareSchemeName name, bool isDeleted, DateTime createdAt)
    {
        Id = id;
        Name = name;
        IsDeleted = isDeleted;
        CreatedAt = createdAt;
    }

    private WelfareScheme()
    {
    }
    
    public static WelfareScheme Create(GenericId id, WelfareSchemeName name, DateTime createdAt)
        => new(id, name, false, createdAt);

    public void Update(WelfareSchemeName name)
    {
        Name = name;
    }
    
    public void Delete()
    {
        IsDeleted = true;
    }
}