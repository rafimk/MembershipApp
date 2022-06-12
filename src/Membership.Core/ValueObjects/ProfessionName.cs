using Membership.Core.Exceptions.Memberships.Professions;

namespace Membership.Core.ValueObjects;

public record ProfessionName
{
    public string Value { get; }

    public ProfessionName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyProfessionNameException();
        }
        
        Value = value;
    }

    public static implicit operator string(ProfessionName name)
        => name.Value;
    
    public static implicit operator ProfessionName(string name)
        => new(name);
}
