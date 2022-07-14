using Membership.Application.DTO.Memberships;
using Membership.Application.DTO.Nationalities;
using Membership.Application.DTO.Users;
using Membership.Core.Entities.Memberships.Disputes;
using Membership.Core.Entities.Memberships.Members;
using Membership.Core.Entities.Memberships.MembershipPeriods;
using Membership.Core.Entities.Memberships.Professions;
using Membership.Core.Entities.Memberships.Qualifications;
using Membership.Core.Entities.Memberships.RegisteredOrganizations;
using Membership.Core.Entities.Memberships.WelfareSchemes;
using Membership.Core.Entities.Nationalities;
using Membership.Core.Entities.Users;

namespace Membership.Infrastructure.DAL.Handlers;

public static class Extensions
{
    public static StateDto AsDto(this State entity)
        => new()
        {
            Id = entity.Id.Value,
            Name = entity.Name,
            CreatedAt = entity.CreatedAt
        };
    
    public static CascadeDto AsCascadeDto(this State entity)
        => new()
        {
            Id = entity.Id.Value,
            Description = entity.Name,
        };
    
    //
    
    public static AreaDto AsDto(this Area entity)
        => new()
        {
            Id = entity.Id.Value,
            Name = entity.Name,
            State = new StateDto
            {
                Id = entity.StateId,
                Name = entity.State?.Name,
                CreatedAt = entity.State.CreatedAt
            },
            CreatedAt = entity.CreatedAt
        };
    
    public static DistrictDto AsDto(this District entity)
        => new()
        {
            Id = entity.Id.Value,
            Name = entity.Name,
            CreatedAt = entity.CreatedAt
        };
    
    public static CascadeDto AsCascadeDto(this District entity)
        => new()
        {
            Id = entity.Id.Value,
            Description = entity.Name,
        };
    
    public static MandalamDto AsDto(this Mandalam entity)
        => new()
        {
            Id = entity.Id.Value,
            Name = entity.Name,
            District = new DistrictDto
            {
                Id = entity.District.Id.Value,
                Name = entity.District.Name,
                CreatedAt = entity.District.CreatedAt
            },
            CreatedAt = entity.CreatedAt
        };
    
    public static CascadeDto AsCascadeDto(this Mandalam entity)
        => new()
        {
            Id = entity.Id.Value,
            Description = entity.Name,
        };
    
    public static PanchayatDto AsDto(this Panchayat entity)
        => new()
        {
            Id = entity.Id.Value,
            Name = entity.Name,
            Mandalam = new MandalamDto
            {
                Id = entity.Mandalam.Id.Value,
                Name = entity.Mandalam.Name,
                CreatedAt = entity.Mandalam.CreatedAt
            },
            CreatedAt = entity.CreatedAt
        };
    
    public static UserDto AsDto(this User entity)
        => new()
        {
            Id = entity.Id.Value,
            FullName = entity.FullName,
            Email = entity.Email,
            MobileNumber = entity.MobileNumber,
            AlternativeContactNumber = entity.AlternativeContactNumber,
            Designation = entity.Designation,
            Role = entity.Role,
            CascadeId = entity.CascadeId,
            CascadeName = entity.CascadeName,
            VerifiedAt = entity.VerifiedAt,
            IsActive = entity.IsActive,
            IsDisputeCommittee = entity.IsDisputeCommittee,
            CreatedAt = entity.CreatedAt
        };

     public static QualificationDto AsDto(this Qualification entity)
        => new()
        {
            Id = entity.Id.Value,
            Name = entity.Name,
            CreatedAt = entity.CreatedAt
        };

    public static ProfessionDto AsDto(this Profession entity)
        => new()
        {
            Id = entity.Id.Value,
            Name = entity.Name,
            CreatedAt = entity.CreatedAt
        };
    
    public static RegisteredOrganizationDto AsDto(this RegisteredOrganization entity)
        => new()
        {
            Id = entity.Id.Value,
            Name = entity.Name,
            CreatedAt = entity.CreatedAt
        };
    
    public static WelfareSchemeDto AsDto(this WelfareScheme entity)
        => new()
        {
            Id = entity.Id.Value,
            Name = entity.Name,
            CreatedAt = entity.CreatedAt
        };
    

    public static MembershipPeriodDto AsDto(this MembershipPeriod entity)
        => new()
        {
            Id = entity.Id.Value,
            Start = entity.Start,
            End = entity.End,
            RegistrationStarted = entity.RegistrationStarted,
            RegistrationEnded = entity.RegistrationEnded,
            IsActive = entity.IsActive,
            CreatedAt = entity.CreatedAt
        };
    
    public static MemberDto AsDto(this Member entity)
        => new()
        {
            Id = entity.Id.Value,
            MembershipId = entity.MembershipId,
            FullName = entity.FullName,
            EmiratesIdNumber = entity.EmiratesIdNumber,
            EmiratesIdExpiry = entity.EmiratesIdExpiry,
            EmiratesIdFrontPage = entity.EmiratesIdFrontPage,
            EmiratesIdLastPage = entity.EmiratesIdLastPage,
            DateOfBirth = entity.DateOfBirth,
            MobileNumber = entity.MobileNumber,
            AlternativeContactNumber = entity.AlternativeContactNumber,
            Email = entity.Email,
            PassportNumber = entity.PassportNumber,
            PassportExpiry = entity.PassportExpiry,
            PassportFrontPage = entity.PassportFrontPage,
            PassportLastPage = entity.PassportLastPage,
            Profession = new ProfessionDto
            {
                Id = entity.ProfessionId,
                Name = entity.Profession?.Name
            },
            Qualification = new QualificationDto
            {
                Id = entity.QualificationId,
                Name = entity.Qualification?.Name
            },
            BloodGroup = (int)entity.BloodGroup,
            Photo = entity.Photo,
            HouseName = entity.HouseName,
            AddressInIndia = entity.AddressInIndia,
            Area = new AreaDto
            {
                Id = entity.AreaId,
                Name = entity.Area?.Name
            },
            Mandalam = new MandalamDto
            {
                Id = entity.MandalamId,
                Name = entity.Mandalam?.Name
            },
            Panchayat = new PanchayatDto
            {
                Id = entity.PanchayatId,
                Name = entity.Panchayat?.Name
            },
            RegisteredOrganizationId = entity.RegisteredOrganizationId,
            WelfareSchemeId = entity.WelfareSchemeId,
            MembershipPeriod = new MembershipPeriodDto
            {
                Id = entity.MembershipPeriodId,
                Start = entity.MembershipPeriod.Start,
                End = entity.MembershipPeriod.End,
                RegistrationStarted = entity.MembershipPeriod.RegistrationStarted,
                RegistrationEnded = entity.MembershipPeriod.RegistrationEnded,
                IsActive = entity.MembershipPeriod.IsActive,
                IsEnrollActive = entity.MembershipPeriod.IsEnrollActive,
                CreatedAt = entity.MembershipPeriod.CreatedAt,
            },
            CollectedAmount = entity.CollectedAmount,
            CreatedBy = entity.CreatedBy,
            IsActive = entity.IsActive,
            CreatedAt = entity.CreatedAt,
            VerifiedAt = entity.VerifiedAt
        };
    
     public static DisputeRequestDto AsDto(this DisputeRequest entity)
        => new()
        {
            Id = entity.Id.Value,
            MemberId = entity.MemberId,
            ProposedAreaId = entity.ProposedAreaId,
            ProposedArea = entity.ProposedArea.AsDto(),
            ProposedMandalamId = entity.ProposedMandalamId,
            ProposedMandalam = entity.ProposedMandalam.AsDto(),
            ProposedPanchayatId = entity.ProposedPanchayatId,
            ProposedPanchayat = entity.ProposedPanchayat.AsDto(),
            Reason = entity.Reason,
            JustificationComment = entity.JustificationComment,
            SubmittedDate = entity.SubmittedDate,
            SubmittedBy = entity.SubmittedBy,
            ActionDate = entity.ActionDate,
            ActionBy = entity.ActionBy,
            Status = (int)entity.Status,
        };
    }
