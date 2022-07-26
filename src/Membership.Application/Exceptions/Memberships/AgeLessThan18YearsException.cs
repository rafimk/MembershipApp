using Membership.Core.Exceptions;

namespace Membership.Application.Exceptions.Memberships;

public class AgeLessThan18YearsException : CustomException
{
    public AgeLessThan18YearsException() : base($"Age Under 18 not allowed")
    {
    }
}