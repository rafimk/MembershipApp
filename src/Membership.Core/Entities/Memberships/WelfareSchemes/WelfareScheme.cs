
namespace Membership.Core.Entities.Memberships.WelfareSchemes;

public class WelfareScheme 
{
    public Guid Id { get; private set; }
    public string Name{ get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private WelfareScheme(Guid id, string name, bool isDeleted, DateTime createdAt)
    {
        Id = id;
        Name = name;
        IsDeleted = isDeleted;
        CreatedAt = createdAt;
    }

    private WelfareScheme()
    {
    }
    
    public static WelfareScheme Create(Guid id, string name, DateTime createdAt)
        => new(id, name, false, createdAt);

    public void Update(string name)
    {
        Name = name;
    }
    
    public void Delete()
    {
        IsDeleted = true;
    }
}