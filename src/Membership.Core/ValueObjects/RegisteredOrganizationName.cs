using Membership.Core.Exceptions.Memberships.RegisteredOrganizations;

namespace Membership.Core.ValueObjects;

public record RegisteredOrganizationName
{
    public string Value { get; }

    public RegisteredOrganizationName(string value)
    {
        Value = value;
    }

    public static implicit operator string(RegisteredOrganizationName name)
        => name.Value;
    
    public static implicit operator RegisteredOrganizationName(string name)
        => new(name);
}
