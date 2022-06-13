﻿using Membership.Core.Consts;
using Membership.Core.Contracts.Memberships;
using Membership.Core.Entities.Memberships.Professions;
using Membership.Core.Entities.Memberships.Qualifications;
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
    public MobileNumber AlternativeContactNumber { get; private set; }
    public Email Email { get; private set; }
    public PassportNumber PassportNumber { get; private set; }
    public Date PassportExpiry { get; private set; }
    public Guid? PassportFrontPage { get; private set; }
    public Guid? PassportLastPage { get; private set; }
    public GenericId ProfessionId { get; private set; }
    public Profession Profession { get; private set; }
    public GenericId QualificationId { get; private set; }
    public Qualification Qualification { get; private set; }
    public BloodGroup BloodGroup { get; private set; }
    public Guid? Photo { get; private set; }
    public string HouseName { get; private set; }
    public string AddressInIndia { get; private set; }
    public string PasswordHash { get; private set; }
    public GenericId AreaId { get; private set; }
    public Area Area { get; private set; }
    public GenericId MandalamId { get; private set; }
    public Mandalam Mandalam { get; private set; }
    public bool IsMemberOfAnyIndianRegisteredOrganization { get; private set; }
    public bool IsKMCCWelfareScheme { get; private set; }
    public double CollectedAmount { get; set; }
    public DateTime CreatedAt { get; private set; }
    public GenericId CreatedBy { get; private set; }
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
        EmiratesIdExpiry = contract.EmiratesIdExpiry;
        EmiratesIdFrontPage = contract.EmiratesIdFrontPage;
        EmiratesIdLastPage = contract.EmiratesIdLastPage;
        DateOfBirth = contract.DateOfBirth;
        MobileNumber = contract.MobileNumber;
        AlternativeContactNumber = contract.AlternativeContactNumber;
        Email = contract.Email;
        PassportNumber = contract.PassportNumber;
        PassportExpiry = contract.PassportExpiry;
        ProfessionId = contract.ProfessionId;
        QualificationId = contract.QualificationId;
        BloodGroup = contract.BloodGroup;
        HouseName = contract.HouseName;
        AddressInIndia = contract.AddressInIndia;
        PasswordHash = contract.PasswordHash;
        AreaId = contract.AreaId;
        MandalamId = contract.MandalamId;
        IsMemberOfAnyIndianRegisteredOrganization = contract.IsMemberOfAnyIndianRegisteredOrganization;
        IsKMCCWelfareScheme = contract.IsKMCCWelfareScheme;
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
            PasswordHash = contract.PasswordHash,
            AreaId = contract.AreaId,
            MandalamId = contract.MandalamId,
            IsMemberOfAnyIndianRegisteredOrganization = contract.IsMemberOfAnyIndianRegisteredOrganization,
            IsKMCCWelfareScheme = contract.IsKMCCWelfareScheme,
            CreatedAt = contract.CreatedAt,
            CreatedBy = contract.CreatedBy,
            IsActive = true
        });

    public void Update(UpdateMemberContract contract)
    {
        EmiratesIdExpiry = contract.EmiratesIdExpiry;
        EmiratesIdFrontPage = contract.EmiratesIdFrontPage;
        EmiratesIdLastPage = contract.EmiratesIdLastPage;
        DateOfBirth = contract.DateOfBirth;
        MobileNumber = contract.MobileNumber;
        AlternativeContactNumber = contract.AlternativeContactNumber;
        Email = contract.Email;
        PassportNumber = contract.PassportNumber;
        PassportExpiry = contract.PassportExpiry;
        ProfessionId = contract.ProfessionId;
        QualificationId = contract.QualificationId;
        BloodGroup = contract.BloodGroup;
        HouseName = contract.HouseName;
        AddressInIndia = contract.AddressInIndia;
        IsMemberOfAnyIndianRegisteredOrganization = contract.IsMemberOfAnyIndianRegisteredOrganization;
        IsKMCCWelfareScheme = contract.IsKMCCWelfareScheme;
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