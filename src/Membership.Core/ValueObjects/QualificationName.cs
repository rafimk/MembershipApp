using Membership.Core.Exceptions.Memberships.Qualifications;

namespace Membership.Core.ValueObjects;

public record QualificationName
{
    public string Value { get; }

    public QualificationName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyQualificationNameException();
        }
        
        Value = value;
    }

    public static implicit operator string(QualificationName name)
        => name.Value;
    
    public static implicit operator QualificationName(string name)
        => new(name);
}
