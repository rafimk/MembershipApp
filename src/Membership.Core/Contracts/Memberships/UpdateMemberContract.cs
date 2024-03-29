﻿using Membership.Core.Consts;
using Membership.Core.ValueObjects;

namespace Membership.Core.Contracts.Memberships;

public record UpdateMemberContract()
{
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
    public Guid? RegisteredOrganizationId { get; set; }
    public Guid? WelfareSchemeId { get; set; }
    public MemberStatus Status {get; set;}
}