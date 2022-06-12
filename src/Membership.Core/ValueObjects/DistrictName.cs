using Membership.Core.Exceptions.Nationalities.Districts;

namespace Membership.Core.ValueObjects;

public record DistrictName
{
    public string Value { get; }

    public DistrictName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyDistrictNameException();
        }
        
        Value = value;
    }

    public static implicit operator string(DistrictName name)
        => name.Value;
    
    public static implicit operator DistrictName(string name)
        => new(name);
}