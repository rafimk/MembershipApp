using Membership.Core.Consts;
using Membership.Core.Entities.Users;

namespace Membership.Core.Entities.Memberships.Members;

public class MembershipVerification
{
    public Guid Id { get; private set; }
    public Guid MemberId { get; private set; }
    public Member Member { get; private set; }
    public Guid EidFrontPage { get; private set; }
    public Guid EidLastPage { get; private set; }
    public Guid? PassportFirstPage { get; private set; }
    public Guid? PassportLastPage { get; private set; }
    public bool EdiFrontAndBackSideValid { get; private set; } 
    public bool EidNumberValid { get; private set; } 
    public bool EidFullNameValid { get; private set; } 
    public bool EidNationalityValid { get; private set; }
    public bool EidDOBValid { get; private set; }
    public bool EidDOEValid { get; private set; }
    public bool EidIssuePlaceValid { get; private set; }
    public bool PassportFirstPageValid { get; private set; }
    public bool PassportLastPageValid { get; private set; }
    public CardType CardType { get; private set; }
    public bool MemberVerified { get; private set; } 
    public Gender Gender { get; set; } = Gender.Male;
    public Guid VerifiedUserId { get; private set; }
    public User VerifiedUser { get; private set; }
    public DateTime? VerifiedAt { get; private set; }

    public MembershipVerification(Guid id, Guid memberId, Guid verifiedUserId, Guid eidFrontPage, 
        Guid eidLastPage, Guid? passportFirstPage, Guid? passportLastPage)
    {
        Id = id;
        MemberId = memberId;
        EidFrontPage = eidFrontPage;
        EidLastPage = eidLastPage;
        PassportFirstPage = passportFirstPage;
        PassportLastPage = passportLastPage;
        EdiFrontAndBackSideValid = false;
        EidNumberValid = false;
        EidFullNameValid = false;
        EidNationalityValid = false;
        EidDOBValid = false;
        EidDOEValid = false;
        EidIssuePlaceValid = false;
        MemberVerified = false;
        Gender = Gender.Male;
        PassportFirstPageValid = false;
        PassportLastPageValid = false;
        CardType = CardType.Unknown;
        VerifiedUserId = verifiedUserId;
        VerifiedAt = null;
    }

    public static MembershipVerification Create(Guid id, Guid memberId, Guid verifiedUserId, Guid eidFrontPage, 
        Guid eidLastPage, Guid? passportFirstPage, Guid? passportLastPage)
        => new(id, memberId, verifiedUserId, eidFrontPage, 
            eidLastPage, passportFirstPage,passportLastPage);

    public void Verified(bool eidFrontAndBackVerified, bool eidNoVerified, bool eidNameVerified, 
        bool eidNationalityVerified, bool eidDOBVerified, bool eidDOEVerified, bool eidIssuePlaceValid, 
        int gender, int cardType, bool passportFirstPageValid, bool passportLastPageValid, DateTime verifiedAt)
    {
        EdiFrontAndBackSideValid = eidFrontAndBackVerified;
        EidNumberValid = eidNoVerified;
        EidFullNameValid = eidNameVerified;
        EidNationalityValid = eidNationalityVerified;
        EidDOBValid = eidDOBVerified;
        EidDOEValid = eidDOEVerified;
        EidIssuePlaceValid = eidIssuePlaceValid;
        PassportFirstPageValid = passportFirstPageValid;
        PassportLastPageValid = passportLastPageValid;
        Gender = (Gender)gender;
        VerifiedAt = verifiedAt;
        CardType = (CardType) cardType;
    }
}