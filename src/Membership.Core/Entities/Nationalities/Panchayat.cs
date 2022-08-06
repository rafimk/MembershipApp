using Membership.Core.Consts;
using Membership.Core.ValueObjects;

namespace Membership.Core.Entities.Nationalities;

public class Panchayat
{
    public Guid Id { get; private set; }
    public PanchayatName Name{ get; private set; }
    public Guid MandalamId { get; private set; }
    public Mandalam Mandalam { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Panchayat(Guid id, PanchayatName name, Guid mandalamId, bool isDeleted, DateTime createdAt)
    {
        Id = id;
        Name = name;
        MandalamId = mandalamId;
        IsDeleted = isDeleted;
        CreatedAt = createdAt;
    }

    public Panchayat()
    {
    }

    public static Panchayat Create(Guid id, PanchayatName name, Guid mandalamId, DateTime createdAt)
        => new(id, name, mandalamId, false, createdAt);
    
    public void Update(PanchayatName name, Guid mandalamId)
    {
        Name = name;
        MandalamId = mandalamId;
    }
    
    public void Delete()
    {
        IsDeleted = true;
    }
}