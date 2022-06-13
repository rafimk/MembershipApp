namespace MMS.Domain.Entities.Nationalities;

public class Panchayat
{
    public GenericId Id { get; private set; }
    public PanchayatName Name{ get; private set; }
    public GenericId MandalamId { get; private set; }
    public Mandalam Mandalam { get; private set; }
    public PanchayatType Type { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Panchayat(GenericId id, PanchayatName name, GenericId mandalamId, PanchayatType type, 
        bool isDeleted, DateTime createdAt)
    {
        Id = id;
        Name = name;
        MandalamId = mandalamId;
        Type = Type;
        IsDeleted = isDeleted;
        CreatedAt = createdAt;
    }

    public Panchayat()
    {
    }

    public static State Create(GenericId id, PanchayatName name, GenericId mandalamId, PanchayatType type, DateTime createdAt)
        => new(id, name, mandalamId, type, false, createdAt);
    
    public void Update(PanchayatName name, GenericId mandalamId, PanchayatType type)
    {
        Name = name;
        MandalamId = mandalamId;
        Type = type;
    }
    
    public void Delete()
    {
        IsDeleted = true;
    }
}