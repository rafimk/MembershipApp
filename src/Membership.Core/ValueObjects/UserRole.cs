using Membership.Core.Exceptions.Users;

namespace Membership.Core.ValueObjects;

public sealed record UserRole
{

    public static IEnumerable<string> AvailableRoles { get; } = new[] {"centralcommittee-admin", "state-admin", "district-admin",
         "mandalam-agent", "district-agent", "dispute-committee", "central-dispute-admin", "monitoring-officer", "verification-officer"};

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
    public static UserRole DistrictAgent() => new("district-agent");
    public static UserRole MandalamAgent() => new("mandalam-agent");
    public static UserRole DisputeCommittee() => new("dispute-committee");
    public static UserRole MonitoringOfficer() => new("monitoring-officer");
    public static UserRole VerificationOfficer() => new("verification-officer");
    public static UserRole CentralDisputeAdmin() => new("central-dispute-admin");
    

    public static implicit operator UserRole(string value) => new UserRole(value);

    public static implicit operator string(UserRole value) => value?.Value;

    public override string ToString() => Value;
}