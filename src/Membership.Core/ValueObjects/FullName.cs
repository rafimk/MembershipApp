using Membership.Core.Exceptions.Common;

namespace Membership.Core.ValueObjects;

public record FullName
{
    public string Value { get; }

    public FullName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyFullNameException();
        }
        
        Value = value;
    }

    public static implicit operator string(FullName name)
        => name.Value;
    
    public static implicit operator FullName(string name)
        => new(name);
}