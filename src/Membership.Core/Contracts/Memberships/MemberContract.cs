﻿using Membership.Core.Consts;
using Membership.Core.ValueObjects;

namespace Membership.Core.Contracts.Memberships;

public record MemberContract()
{
    public Guid Id { get; set; }
    public MembershipId MembershipId { get; set; }
    public FullName FullName { get; set; } 
    public EmiratesIdNumber EmiratesIdNumber { get; set; }
    public DateTime EmiratesIdExpiry { get; set; }
    public Guid? EmiratesIdFrontPage { get; set;}
    public Guid? EmiratesIdLastPage { get; set;}
    public DateTime DateOfBirth { get; set; } 
    public MobileNumber MobileNumber { get; set; }
    public string Email { get; set; }
    public string PassportNumber { get; set; }
    public DateTime? PassportExpiry { get; set; }
    public Guid? PassportFrontPage { get; set;}
    public Guid? PassportLastPage { get; set;}
    public Guid? ProfessionId { get; set; }
    public Guid? QualificationId { get; set; }
    public BloodGroup BloodGroup { get; set; }
    public Gender Gender { get; set; }
    public Guid? Photo { get; set;}
    public string HouseName { get; set; }
    public string AddressInIndia { get; set; }
    public Guid? AddressInDistrictId { get; set; }
    public Guid? AddressInMandalamId { get; set; }
    public Guid? AddressInPanchayatId { get; set; }
    public string PasswordHash { get; set; }
    public Guid StateId { get; set; }
    public Guid AreaId { get; set; }
    public Guid DistrictId { get; set; } 
    public Guid MandalamId { get; set; } 
    public Guid PanchayatId { get; set;}
    public Guid? RegisteredOrganizationId { get; set; }
    public Guid? WelfareSchemeId { get; set; }
    public Guid MembershipPeriodId { get; set; }
    public string CardNumber { get; set; }
    public DateTime CreatedAt { get; set; } 
    public Guid CreatedBy { get; set; }
    public bool IsActive { get; set; }
    public bool ManuallyEntered { get; set; }
    public Guid? AgentId { get; set; }
    public string MembershipNoPrefix { get; set; }
}