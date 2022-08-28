using Membership.Core.Contracts.Users;
using Membership.Core.Entities.Nationalities;
using Membership.Core.Exceptions.Users;
using Membership.Core.ValueObjects;

namespace Membership.Core.Entities.Users;

public class User
{
    public Guid Id { get; private set; }
    public FullName FullName { get; private set; }
    public Email Email { get; private set; }
    public MobileNumber MobileNumber { get; private set; }
    public string Designation { get; private set; }
    public string PasswordHash { get; private set; }
    public UserRole Role { get; private set; }
    public Guid? StateId { get; private set; }
    public State State { get; private set; }
    public Guid? DistrictId { get; private set; }
    public District District { get; private set; }
    
    public Guid? MandalamId { get; private set; }
    public Mandalam Mandalam { get; private set; }
    public Guid? CascadeId { get; private set; }
    public string CascadeName { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool IsActive {get; private set;}
    public bool IsDisputeCommittee {get; private set;}
    public DateTime? VerifiedAt { get; private set; }
  

    public User()
    {
    }

    private User(UserContract contract)
    {
        Id = contract.Id;
        FullName = contract.FullName;
        Email = contract.Email;
        MobileNumber = contract.MobileNumber;
        Designation = contract.Designation;
        PasswordHash = contract.PasswordHash;
        Role = contract.Role;
        StateId = contract.StateId;
        DistrictId = contract.DistrictId;
        MandalamId = contract.MandalamId;
        CascadeId = contract.CascadeId;
        CascadeName = contract.CascadeName;
        CreatedAt = contract.CreatedAt;
        IsActive = contract.IsActive;
        IsDisputeCommittee = contract.IsDisputeCommittee;
    }

    public static User Create(UserCreateContract contract)
        => new(new UserContract
        {
            Id = contract.Id,
            FullName = contract.FullName,
            Email = contract.Email,
            MobileNumber = contract.MobileNumber,
            Designation = contract.Designation,
            PasswordHash = contract.PasswordHash,
            Role = contract.Role,
            StateId = contract.StateId,
            DistrictId = contract.DistrictId,
            MandalamId = contract.MandalamId,
            CascadeId = contract.CascadeId,
            CascadeName = contract.CascadeName,
            CreatedAt = contract.CreatedAt,
            IsDisputeCommittee = contract.IsDisputeCommittee,
            IsActive = true
        });

    public void Update(UpdateCreateContract contract)
    {
        FullName = contract.FullName;
        MobileNumber = contract.MobileNumber;
        Designation = contract.Designation;
        IsDisputeCommittee = contract.IsDisputeCommittee;
    }
    
    public void ChangePassword(string newPasswordHash, string email)
    {
        Email = new Email(email);
        PasswordHash = newPasswordHash;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Verify(DateTime verifiedAt, string password)
    {
        if (VerifiedAt.HasValue)
        {
            throw new UserAlreadyVerifiedException(Email);
        }

        PasswordHash = password;
        VerifiedAt = verifiedAt;
    }
}