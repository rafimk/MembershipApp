using Membership.Core.Exceptions.Nationalities.Areas;

namespace Membership.Core.ValueObjects;

public record AreaName
{
    public string Value { get; }

    public AreaName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyAreaNameException();
        }
        
        Value = value;
    }

    public static implicit operator string(AreaName name)
        => name.Value;
    
    public static implicit operator AreaName(string name)
        => new(name);
}