using Membership.Core.ValueObjects;

namespace Membership.Core.Entities.Memberships.Qualifications;

public class Qualification 
{
    public GenericId Id { get; private set; }
    public QualificationName Name{ get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Qualification(GenericId id, QualificationName name, bool isDeleted, DateTime createdAt)
    {
        Id = id;
        Name = name;
        IsDeleted = isDeleted;
        CreatedAt = createdAt;
    }

    private Qualification()
    {
    }
    
    public static Qualification Create(GenericId id, QualificationName name, DateTime createdAt)
        => new(id, name, false, createdAt);

    public void Update(QualificationName name)
    {
        Name = name;
    }
    
    public void Delete()
    {
        IsDeleted = true;
    }
}