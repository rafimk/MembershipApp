using Membership.Core.Exceptions.Memberships;

namespace Membership.Core.ValueObjects;

public record EmiratesIdNumber
{
    public string Value { get; }

    public EmiratesIdNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyEmiratesIdNumberException();
        }
        
        Value = value;
    }

    public static implicit operator string(EmiratesIdNumber emiratesIdNumber)
        => emiratesIdNumber.Value;
    
    public static implicit operator EmiratesIdNumber(string emiratesIdNumber)
        => new(emiratesIdNumber);
}