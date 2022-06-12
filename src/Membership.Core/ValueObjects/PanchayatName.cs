using Membership.Core.Exceptions.Nationalities.Panchayats;

namespace Membership.Core.ValueObjects;

public record PanchayatName
{
    public string Value { get; }

    public PanchayatName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyPanchayatNameException();
        }
        
        Value = value;
    }

    public static implicit operator string(PanchayatName name)
        => name.Value;
    
    public static implicit operator PanchayatName(string name)
        => new(name);
}