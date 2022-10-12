namespace Membership.Infrastructure.DAL.ReadModels;

public sealed class UserReadModel
{
    public Guid Id { get; private set; }
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public string MobileNumber { get; private set; }
    public string Designation { get; private set; }
    public string Role { get; private set; }
    public Guid? StateId { get; private set; }
    public StateReadModel State { get; private set; }
    public Guid? DistrictId { get; private set; }
    public DistrictReadModel District { get; private set; }
    
    public Guid? MandalamId { get; private set; }
    public MandalamReadModel Mandalam { get; private set; }
    public Guid? CascadeId { get; private set; }
    public string CascadeName { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool IsActive {get; private set;}
    public bool IsDeleted {get; private set;}
    public DateTime? VerifiedAt { get; private set; }
}