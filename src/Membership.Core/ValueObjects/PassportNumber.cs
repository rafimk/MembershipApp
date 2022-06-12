using Membership.Core.Exceptions.Memberships;

namespace Membership.Core.ValueObjects;

public record PassportNumber
{
    public string Value { get; }

    public PassportNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyPassportNumberException();
        }
        
        Value = value;
    }

    public static implicit operator string(PassportNumber passportNumber)
        => passportNumber.Value;
    
    public static implicit operator PassportNumber(string passportNumber)
        => new(passportNumber);
}