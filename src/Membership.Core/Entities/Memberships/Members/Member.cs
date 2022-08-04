using Membership.Core.Consts;
using Membership.Core.Contracts.Memberships;
using Membership.Core.Entities.Memberships.MembershipPeriods;
using Membership.Core.Entities.Memberships.Professions;
using Membership.Core.Entities.Memberships.Qualifications;
using Membership.Core.Entities.Memberships.RegisteredOrganizations;
using Membership.Core.Entities.Memberships.WelfareSchemes;
using Membership.Core.Entities.Nationalities;
using Membership.Core.Exceptions.Users;
using Membership.Core.ValueObjects;

namespace Membership.Core.Entities.Memberships.Members;

public class Member
{
    public GenericId Id { get; private set; }
    public MembershipId MembershipId { get; private set; }
    public FullName FullName { get; private set; }
    public EmiratesIdNumber EmiratesIdNumber { get; private set; }
    public Date EmiratesIdExpiry { get; private set; }
    public Guid? EmiratesIdFrontPage { get; private set; }
    public Guid? EmiratesIdLastPage { get; private set; }
    public Date DateOfBirth { get; private set; }
    public MobileNumber MobileNumber { get; private set; }
    public OptionalMobileNumber AlternativeContactNumber { get; private set; }
    public string Email { get; private set; }
    public string PassportNumber { get; private set; }
    public DateTime? PassportExpiry { get; private set; }
    public Guid? PassportFrontPage { get; private set; }
    public Guid? PassportLastPage { get; private set; }
    public Guid? ProfessionId { get; private set; }
    public Profession Profession { get; private set; }
    public Guid? QualificationId { get; private set; }
    public Qualification Qualification { get; private set; }
    public BloodGroup BloodGroup { get; private set; }
    public Gender Gender { get; set; } = Gender.Male;
    public Guid? Photo { get; private set; }
    public string HouseName { get; private set; }
    public string AddressInIndia { get; private set; }
    public string PasswordHash { get; private set; }
    
    public Guid? AddressInDistrictId { get; private set; }
    public District AddressInDistrict { get; private set; }
    public Guid? AddressInMandalamId { get; private set; }
    public Mandalam AddressInMandalam { get; private set; }
    public Guid? AddressInPanchayatId { get; private set; }
    public Panchayat AddressInPanchayat { get; private set; }
    
    public Guid StateId { get; private set; }
    public State State { get; private set; }
    public GenericId AreaId { get; private set; }
    public Area Area { get; private set; }
    public Guid DistrictId { get; private set; }
    public District District { get; private set; }
    public GenericId MandalamId { get; private set; }
    public Mandalam Mandalam { get; private set; }
    public GenericId PanchayatId { get; private set; }
    public Panchayat Panchayat { get; private set; }
    public Guid? RegisteredOrganizationId { get; private set; }
    public RegisteredOrganization RegisteredOrganization { get; private set; }
    public Guid? WelfareSchemeId { get; private set; }
    public WelfareScheme WelfareScheme { get; private set; }
    public double CollectedAmount { get; private set; }
    public MemberStatus Status {get; private set;}
    public string CardNumber { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public GenericId CreatedBy { get; private set; }
    public GenericId MembershipPeriodId { get; private set; }
    public MembershipPeriod MembershipPeriod { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime? VerifiedAt { get; private set; }

    public Member()
    {
    }

    private Member(MemberContract contract)
    {
        Id = contract.Id;
        MembershipId = contract.MembershipId;
        FullName = contract.FullName;
        EmiratesIdNumber = contract.EmiratesIdNumber;
        EmiratesIdExpiry = new Date(contract.EmiratesIdExpiry);
        EmiratesIdFrontPage = contract.EmiratesIdFrontPage;
        EmiratesIdLastPage = contract.EmiratesIdLastPage;
        DateOfBirth = new Date(contract.DateOfBirth);
        MobileNumber = contract.MobileNumber;
        AlternativeContactNumber = contract.AlternativeContactNumber;
        Email = contract.Email;
        PassportNumber = contract.PassportNumber;
        PassportExpiry = contract.PassportExpiry;
        ProfessionId = contract.ProfessionId;
        QualificationId = contract.QualificationId;
        BloodGroup = contract.BloodGroup;
        Gender = contract.Gender;
        PassportFrontPage = contract.PassportFrontPage;
        PassportLastPage = contract.PassportLastPage;
        Photo = contract.Photo;
        HouseName = contract.HouseName;
        AddressInIndia = contract.AddressInIndia;
        AddressInDistrictId = contract.AddressInDistrictId;
        AddressInMandalamId = contract.AddressInMandalamId;
        AddressInPanchayatId = contract.AddressInPanchayatId;
        PasswordHash = contract.PasswordHash;
        StateId = contract.StateId;
        AreaId = contract.AreaId;
        DistrictId = contract.DistrictId;
        MandalamId = contract.MandalamId;
        PanchayatId = contract.PanchayatId;
        RegisteredOrganizationId = contract.RegisteredOrganizationId;
        WelfareSchemeId = contract.WelfareSchemeId;
        MembershipPeriodId = contract.MembershipPeriodId;
        CardNumber = contract.CardNumber;
        Status = MemberStatus.Completed;
        CreatedAt = contract.CreatedAt;
        CreatedBy = contract.CreatedBy;
        IsActive = contract.IsActive;
    }

    public static Member Create(CreateMemberContract contract)
        => new(new MemberContract
        {
            Id = contract.Id,
            MembershipId = contract.MembershipId,
            FullName = contract.FullName,
            EmiratesIdNumber = contract.EmiratesIdNumber,
            EmiratesIdExpiry = contract.EmiratesIdExpiry,
            EmiratesIdFrontPage = contract.EmiratesIdFrontPage,
            EmiratesIdLastPage = contract.EmiratesIdLastPage,
            DateOfBirth = contract.DateOfBirth,
            MobileNumber = contract.MobileNumber,
            AlternativeContactNumber = contract.AlternativeContactNumber,
            Email = contract.Email,
            PassportNumber = contract.PassportNumber,
            PassportExpiry = contract.PassportExpiry,
            ProfessionId = contract.ProfessionId,
            QualificationId = contract.QualificationId,
            BloodGroup = contract.BloodGroup,
            HouseName = contract.HouseName,
            AddressInIndia = contract.AddressInIndia,
            AddressInDistrictId = contract.AddressInDistrictId,
            AddressInMandalamId = contract.AddressInMandalamId,
            AddressInPanchayatId = contract.AddressInPanchayatId,
            PasswordHash = contract.PasswordHash,
            StateId = contract.StateId,
            AreaId = contract.AreaId,
            DistrictId = contract.DistrictId,
            MandalamId = contract.MandalamId,
            PanchayatId = contract.PanchayatId,
            RegisteredOrganizationId = contract.RegisteredOrganizationId,
            WelfareSchemeId = contract.WelfareSchemeId,
            MembershipPeriodId = contract.MembershipPeriodId,
            CardNumber = contract.CardNumber,
            CreatedAt = contract.CreatedAt,
            CreatedBy = contract.CreatedBy,
            IsActive = true
        });

    public void Update(UpdateMemberContract contract)
    {
        EmiratesIdExpiry = new Date(contract.EmiratesIdExpiry);
        EmiratesIdFrontPage = contract.EmiratesIdFrontPage;
        EmiratesIdLastPage = contract.EmiratesIdLastPage;
        DateOfBirth = new Date(contract.DateOfBirth);
        MobileNumber = contract.MobileNumber;
        AlternativeContactNumber = contract.AlternativeContactNumber;
        Email = contract.Email;
        PassportNumber = contract.PassportNumber;
        PassportExpiry = contract.PassportExpiry;
        PassportFrontPage = contract.PassportFrontPage;
        PassportLastPage = contract.PassportLastPage;
        Photo = contract.Photo;
        ProfessionId = contract.ProfessionId;
        QualificationId = contract.QualificationId;
        BloodGroup = contract.BloodGroup;
        Gender = contract.Gender;
        HouseName = contract.HouseName;
        Status = contract.Status;
        AddressInIndia = contract.AddressInIndia;
        AddressInDistrictId = contract.AddressInDistrictId;
        AddressInMandalamId = contract.AddressInMandalamId;
        AddressInPanchayatId = contract.AddressInPanchayatId;
        RegisteredOrganizationId = contract.RegisteredOrganizationId;
        WelfareSchemeId = contract.WelfareSchemeId;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Verify(DateTime verifiedAt)
    {
        if (VerifiedAt.HasValue)
        {
            throw new UserAlreadyVerifiedException(Email);
        }

        VerifiedAt = verifiedAt;
    }
}