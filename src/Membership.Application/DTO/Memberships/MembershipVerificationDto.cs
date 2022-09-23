using Membership.Application.DTO.Users;

namespace Membership.Application.DTO.Memberships;

public class MembershipVerificationDto
{
    public Guid Id { get; set; }
    public Guid MemberId { get; set; }
    public MemberBasicDto Member { get; set; }
    public Guid EidFrontPage { get; set; }
    public Guid EidLastPage { get; set; }
    public Guid? PassportFirstPage { get; set; }
    public Guid? PassportLastPage { get; set; }
    public bool EdiFrontAndBackSideValid { get; set; } 
    public bool EidNumberValid { get; set; } 
    public bool EidFullNameValid { get; set; } 
    public bool EidNationalityValid { get; set; }
    public bool EidDOBValid { get; set; }
    public bool EidDOEValid { get; set; }
    public bool EidIssuePlaceValid { get; set; }
    public bool MemberVerified { get; set; }
    public bool PassportFirstPageValid { get; set; }
    public bool PassportLastPageValid { get; set; }
    public int Gender { get; set; } 
    public Guid VerifiedUserId { get; set; }
    public string VerifiedUserName { get; set; }
    public DateTime? VerifiedAt { get; set; }
}