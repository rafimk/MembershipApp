using Membership.Core.Consts;
using Membership.Core.ValueObjects;

namespace Membership.Core.Entities.Nationalities;

public class Panchayat
{
    public GenericId Id { get; private set; }
    public PanchayatName Name{ get; private set; }
    public GenericId MandalamId { get; private set; }
    public Mandalam Mandalam { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Panchayat(GenericId id, PanchayatName name, GenericId mandalamId, bool isDeleted, DateTime createdAt)
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

    public static Panchayat Create(GenericId id, PanchayatName name, GenericId mandalamId, DateTime createdAt)
        => new(id, name, mandalamId, false, createdAt);
    
    public void Update(PanchayatName name, GenericId mandalamId)
    {
        Name = name;
        MandalamId = mandalamId;
    }
    
    public void Delete()
    {
        IsDeleted = true;
    }
}