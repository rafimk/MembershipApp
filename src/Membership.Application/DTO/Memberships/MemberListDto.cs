using Membership.Application.DTO.Nationalities;

namespace Membership.Application.DTO.Memberships;

public class MemberListDto
{
    public Guid Id { get; set; }
    public string MembershipId { get; set; }
    public string FullName { get; set; }
    public string EmiratesIdNumber { get; set; }
    public DateTime EmiratesIdExpiry { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string MobileNumber { get; set; }
    public string Email { get; set; }
    public string PassportNumber { get; set; }
    public Guid StateId { get; set; }
    public Guid AreaId { get; set; }
    public AreaDto Area { get; set; }
    public Guid DistrictId { get; set; }
    public Guid MandalamId { get; set; }
    public MandalamDto Mandalam { get; set; }
    public Guid PanchayatId { get; set; }
    public PanchayatDto Panchayat { get; set; }
    public int Status {get; set;}
    public string CardNumber { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid MembershipPeriodId { get; set; }
    public bool IsActive { get; set; }
    public DateTime? VerifiedAt { get; set; }
    public bool ManuallyEntered { get; set; }
}