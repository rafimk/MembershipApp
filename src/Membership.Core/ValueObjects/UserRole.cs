namespace Membership.Core.ValueObjects;

public sealed record UserRole
{

    public static IEnumerable<string> AvailableRoles { get; } = new[] {"centralcommittee-admin", 
        "state-admin", "district-admin", "mandalam-agent"};

    public string Value { get; }

    public UserRole(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > 30)
        {
            throw new InvalidRoleException(value);
        }

        if (!AvailableRoles.Contains(value))
        {
            throw new InvalidRoleException(value);
        }

        Value = value;
    }

    public static UserRole CentralCommitteeAdmin() => new("centralcommittee-admin");
    public static UserRole StateAdmin() => new("state-admin");
    public static UserRole DistrictAdmin() => new("district-admin");
    public static UserRole MandalamAgent() => new("mandalam-agent");

    public static implicit operator Role(string value) => new Role(value);

    public static implicit operator string(Role value) => value?.Value;

    public override string ToString() => Value;
}