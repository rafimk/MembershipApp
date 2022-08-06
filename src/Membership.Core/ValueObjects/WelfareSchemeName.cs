using Membership.Core.Exceptions.Memberships.RegisteredOrganizations;

namespace Membership.Core.ValueObjects;

public record WelfareSchemeName
{
    public string Value { get; }

    public WelfareSchemeName(string value)
    {
        Value = value;
    }

    public static implicit operator string(WelfareSchemeName name)
        => name.Value;
    
    public static implicit operator WelfareSchemeName(string name)
        => new(name);
}
