namespace Membership.Core.Exceptions.Memberships;

internal sealed class MemberVerificationAlreadyInitiated : CustomException
{

    public MemberVerificationAlreadyInitiated() : base("Member verification already initiated.")
    {
    }
}