using Membership.Core.ValueObjects;

namespace Membership.Core.Entities.Memberships.RegisteredOrganizations;

public class RegisteredOrganization 
{
    public GenericId Id { get; private set; }
    public RegisteredOrganizationName Name{ get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private RegisteredOrganization(GenericId id, RegisteredOrganizationName name, bool isDeleted, DateTime createdAt)
    {
        Id = id;
        Name = name;
        IsDeleted = isDeleted;
        CreatedAt = createdAt;
    }

    private RegisteredOrganization()
    {
    }
    
    public static RegisteredOrganization Create(GenericId id, RegisteredOrganizationName name, DateTime createdAt)
        => new(id, name, false, createdAt);

    public void Update(RegisteredOrganizationName name)
    {
        Name = name;
    }
    
    public void Delete()
    {
        IsDeleted = true;
    }
}