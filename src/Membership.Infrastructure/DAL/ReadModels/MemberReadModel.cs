using Membership.Core.Consts;
namespace Membership.Infrastructure.DAL.ReadModels;

public sealed class MemberReadModel
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
    public AreaReadModel Area { get; set; }
    public Guid DistrictId { get; set; }
    public Guid MandalamId { get; set; }
    public MandalamReadModel Mandalam { get; set; }
    public Guid PanchayatId { get; set; }
    public PanchayatReadModel Panchayat { get; set; }
    public MemberStatus Status {get; set;}
    public string CardNumber { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid MembershipPeriodId { get; set; }
    public bool IsActive { get; set; }
    public DateTime? VerifiedAt { get; set; }
    public bool ManuallyEntered { get; set; }
}