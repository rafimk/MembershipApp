namespace Membership.Core.Exceptions.Memberships;

internal sealed class MemberVerificationAlreadyCompleted : CustomException
{

    public MemberVerificationAlreadyCompleted() : base("Member verification already completed.")
    {
    }
}