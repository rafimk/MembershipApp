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
            Id = entity.Id,
            Name = entity.Name,
            Prefix = entity.Prefix,
            CreatedAt = entity.CreatedAt
        };
    
    public static CascadeDto AsCascadeDto(this State entity)
        => new()
        {
            Id = entity.Id,
            Description = entity.Name,
        };
    
    //
    
    public static AreaDto AsDto(this Area entity)
        => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            State = new StateDto
            {
                Id = entity.StateId,
                Name = entity.State?.Name,
                CreatedAt = entity.State.CreatedAt
            },
            CreatedAt = entity.CreatedAt
        };
    
    public static AreaDto AsBasicDto(this Area entity)
        => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            State = null,
            CreatedAt = entity.CreatedAt
        };
    
    public static DistrictDto AsDto(this District entity)
        => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            CreatedAt = entity.CreatedAt
        };
    
    public static CascadeDto AsCascadeDto(this District entity)
        => new()
        {
            Id = entity.Id,
            Description = entity.Name,
        };
    
    public static MandalamDto AsDto(this Mandalam entity)
        => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            District = new DistrictDto
            {
                Id = entity.District.Id,
                Name = entity.District.Name,
                CreatedAt = entity.District.CreatedAt
            },
            CreatedAt = entity.CreatedAt
        };
    
    public static MandalamDto AsBaicDto(this Mandalam entity)
        => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            District = null,
            CreatedAt = entity.CreatedAt
        };
    
    public static CascadeDto AsCascadeDto(this Mandalam entity)
        => new()
        {
            Id = entity.Id,
            Description = entity.Name,
        };
    
    public static PanchayatDto AsDto(this Panchayat entity)
        => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Mandalam = new MandalamDto
            {
                Id = entity.Mandalam.Id,
                Name = entity.Mandalam.Name,
                CreatedAt = entity.Mandalam.CreatedAt
            },
            CreatedAt = entity.CreatedAt
        };
    
    public static PanchayatDto AsBaicDto(this Panchayat entity)
        => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Mandalam = null,
            CreatedAt = entity.CreatedAt
        };
    
    public static UserDto AsDto(this User entity)
        => new()
        {
            Id = entity.Id,
            FullName = entity.FullName,
            Email = entity.Email,
            MobileNumber = entity.MobileNumber,
            Designation = entity.Designation,
            Role = entity.Role,
            CascadeId = entity.CascadeId,
            StateId = entity.StateId,
            DistrictId = entity.DistrictId,
            CascadeName = entity.CascadeName,
            VerifiedAt = entity.VerifiedAt,
            IsActive = entity.IsActive,
            IsDisputeCommittee = entity.IsDisputeCommittee,
            CreatedAt = entity.CreatedAt
        };

     public static QualificationDto AsDto(this Qualification entity)
        => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            CreatedAt = entity.CreatedAt
        };

    public static ProfessionDto AsDto(this Profession entity)
        => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            CreatedAt = entity.CreatedAt
        };
    
    public static RegisteredOrganizationDto AsDto(this RegisteredOrganization entity)
        => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            CreatedAt = entity.CreatedAt
        };
    
    public static WelfareSchemeDto AsDto(this WelfareScheme entity)
        => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            CreatedAt = entity.CreatedAt
        };
    

    public static MembershipPeriodDto AsDto(this MembershipPeriod entity)
        => new()
        {
            Id = entity.Id,
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
            Id = entity.Id,
            MembershipId = $"{entity.MembershipNoPrefix.Trim()}{entity.MembershipSequenceNo.ToString("D6")}",
            FullName = entity.FullName,
            EmiratesIdNumber = entity.EmiratesIdNumber,
            EmiratesIdExpiry = entity.EmiratesIdExpiry,
            EmiratesIdFrontPage = entity.EmiratesIdFrontPage,
            EmiratesIdLastPage = entity.EmiratesIdLastPage,
            DateOfBirth = entity.DateOfBirth,
            MobileNumber = entity.MobileNumber,
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
            Gender = (int)entity.Gender,
            Photo = entity.Photo,
            HouseName = entity.HouseName,
            AddressInIndia = entity.AddressInIndia,
            State = new StateDto
            {
                Id = entity.Area.StateId,
                Name = entity.Area.State?.Name
            },
            Area = new AreaDto
            {
                Id = entity.AreaId,
                Name = entity.Area?.Name
            },
            District = new DistrictDto
            {
                Id = entity.Mandalam.DistrictId,
                Name = entity.Mandalam.District.Name
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
            Id = entity.Id,
            MemberId = entity.MemberId,
            Member = entity.Member.AsBasicDto(),
            ToState = entity.ToState.AsDto(),
            ToArea = entity.ToArea.AsBasicDto(),
            ToDistrict = entity.ToDistrict.AsDto(),
            ToMandalam = entity.ToMandalam.AsBaicDto(),
            ToPanchayat = entity.ToPanchayat.AsBaicDto(),
            FromState = entity.FromState.AsDto(),
            FromArea = entity.FromArea.AsBasicDto(),
            FromDistrict = entity.FromDistrict.AsDto(),
            FromMandalam = entity.FromMandalam.AsBaicDto(),
            FromPanchayat = entity.FromPanchayat.AsBaicDto(),
            Reason = entity.Reason,
            JustificationComment = entity.JustificationComment,
            SubmittedDate = entity.SubmittedDate,
            SubmittedBy = entity.SubmittedBy,
            ActionDate = entity.ActionDate,
            ActionBy = entity.ActionBy,
            Status = (int)entity.Status,
        };
     
     public static DisputeRequestDto AsDtoWithMember(this DisputeRequest entity)
         => new()
         {
             Id = entity.Id,
             MemberId = entity.MemberId,
             Member = entity.Member.AsBasicDto(),
             ToState = entity.ToState.AsDto(),
             ToArea = entity.ToArea.AsBasicDto(),
             ToDistrict = entity.ToDistrict.AsDto(),
             ToMandalam = entity.ToMandalam.AsBaicDto(),
             ToPanchayat = entity.ToPanchayat.AsBaicDto(),
             FromState = entity.FromState.AsDto(),
             FromArea = entity.FromArea.AsBasicDto(),
             FromDistrict = entity.FromDistrict.AsDto(),
             FromMandalam = entity.FromMandalam.AsBaicDto(),
             FromPanchayat = entity.FromPanchayat.AsBaicDto(),
             Reason = entity.Reason,
             JustificationComment = entity.JustificationComment,
             SubmittedDate = entity.SubmittedDate,
             SubmittedBy = entity.SubmittedBy,
             ActionDate = entity.ActionDate,
             ActionBy = entity.ActionBy,
             Status = (int)entity.Status,
         };
     
     public static MemberBasicDto AsBasicDto(this Member entity)
         => new()
         {
            Id = entity.Id,
            MembershipId = $"{entity.MembershipNoPrefix.Trim()}{entity.MembershipSequenceNo.ToString("D6")}",
            FullName = entity.FullName,
            EmiratesIdNumber = entity.EmiratesIdNumber,
            EmiratesIdExpiry = entity.EmiratesIdExpiry,
            DateOfBirth = entity.DateOfBirth,
            MobileNumber = entity.MobileNumber,
            Email = entity.Email,
            BloodGroup = (int)entity.BloodGroup,
            Gender = (int)entity.Gender,
            HouseName = entity.HouseName,
            AddressInIndia = entity.AddressInIndia,
            State = entity.State?.Name
         };
     
     public static MembershipVerificationDto AsDto(this MembershipVerification entity)
         => new()
         {
             Id = entity.Id,
            MemberId = entity.MemberId,
            Member = entity.Member.AsBasicDto(),
            EidFrontPage = entity.EidFrontPage,
            EidLastPage = entity.EidLastPage,
            PassportFirstPage = entity.PassportFirstPage,
            PassportLastPage = entity.PassportLastPage,
            EdiFrontAndBackSideValid = entity.EdiFrontAndBackSideValid,
            EidNumberValid = entity.EidNumberValid,
            EidFullNameValid = entity.EidFullNameValid,
            EidNationalityValid = entity.EidNationalityValid,
            EidDOBValid = entity.EidDOBValid,
            EidDOEValid = entity.EidDOEValid,
            EidIssuePlaceValid = entity.EidIssuePlaceValid,
            MemberVerified = entity.MemberVerified,
            PassportFirstPageValid = entity.PassportFirstPageValid,
            PassportLastPageValid = entity.PassportLastPageValid,
            Gender = (int)entity.Gender,
            VerifiedUserId = entity.VerifiedUserId,
            VerifiedUserName = "",
            VerifiedAt = entity.VerifiedAt
         };
    }
