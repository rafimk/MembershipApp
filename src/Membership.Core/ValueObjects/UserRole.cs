namespace Membership.Core.ValueObjects;

public sealed record UserRole
{
    public string Value { get; }

    public const string CentralCommitteeAdmin = nameof(CentralCommitteeAdmin);
    public const string StateAdmin = nameof(StateAdmin);
    public const string DistrictAdmin = nameof(DistrictAdmin);
    public const string MandalamAgent = nameof(MandalamAgent);

    public UserRole(string value)
        => Value = value;

    public static implicit operator string(UserRole jobTitle)
        => jobTitle.Value;

    public static implicit operator UserRole(string value)
        => new(value);
}