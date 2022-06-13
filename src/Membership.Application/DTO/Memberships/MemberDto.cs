﻿using Membership.Application.DTO.Nationalities;
using Membership.Core.ValueObjects;

namespace Membership.Application.DTO.Memberships;

public class MemberDto
{
    public Guid Id { get; set; }
    public string MembershipId { get; set; }
    public string FullName { get; set; }
    public string EmiratesIdNumber { get; set; }
    public DateTimeOffset EmiratesIdExpiry { get; set; }
    public Guid? EmiratesIdFrontPage { get; set; }
    public Guid? EmiratesIdLastPage { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public string MobileNumber { get; set; }
    public string AlternativeContactNumber { get; set; }
    public string Email { get; set; }
    public string PassportNumber { get; set; }
    public DateTimeOffset PassportExpiry { get; set; }
    public Guid? PassportFrontPage { get; set; }
    public Guid? PassportLastPage { get; set; }
    public Guid ProfessionId { get; set; }
    public ProfessionDto Profession { get; set; }
    public Guid QualificationId { get; set; }
    public QualificationDto Qualification { get; set; }
    public int BloodGroup { get; set; }
    public Guid? Photo { get; set; }
    public string HouseName { get; set; }
    public string AddressInIndia { get; set; }
    public string PasswordHash { get; set; }
    public Guid AreaId { get; set; }
    public AreaDto Area { get; set; }
    public GenericId MandalamId { get; set; }
    public MandalamDto Mandalam { get; set; }
    public bool IsMemberOfAnyIndianRegisteredOrganization { get; set; }
    public bool IsKMCCWelfareScheme { get; set; }
    public double CollectedAmount { get; set; }
    public DateTime CreatedAt { get; set; }
    public GenericId CreatedBy { get; set; }
    public bool IsActive { get; set; }
    public DateTime? VerifiedAt { get; set; }
}