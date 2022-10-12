using Membership.Application.DTO.Memberships;
using Membership.Application.DTO.Nationalities;
using Membership.Application.DTO.Users;

namespace Membership.Infrastructure.DAL.ReadModels;

public static class Extensions
{
     public static MemberListDto AsDto(this MemberReadModel entity)
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
            PassportNumber = entity.PassportNumber,
            StateId = entity.StateId,
            AreaId = entity.AreaId,
            DistrictId = entity.DistrictId,
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
            CreatedBy = entity.CreatedBy,
            IsActive = entity.IsActive,
            CreatedAt = entity.CreatedAt,
            VerifiedAt = entity.VerifiedAt,
            Agent = entity.Agent?.FullName
        };
     
     public static UserDto AsDto(this UserReadModel entity)
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
             IsDisputeCommittee = false,
             CreatedAt = entity.CreatedAt
         };
     
     public static DisputeRequestListDto AsDto(this DisputeRequestReadModel entity)
         => new()
         {
             Id = entity.Id,
             MemberId = entity.MemberId,
             MembershipId = $"{entity.Member.MembershipNoPrefix.Trim()}{entity.Member.MembershipSequenceNo.ToString("D6")}",
             FullName = entity.Member?.FullName,
             EmiratesIdNumber = entity.Member?.EmiratesIdNumber,
             MobileNumber = entity.Member?.MobileNumber,
             ToState = new StateDto
             {
                 Id = entity.ToStateId,
                 Name = entity.ToState?.Name
             },
             ToArea = new AreaDto
             {
                 Id = entity.ToAreaId,
                 Name = entity.ToArea?.Name
             },
             ToDistrict = new DistrictDto
             {
                 Id = entity.ToDistrictId,
                 Name = entity.ToDistrict?.Name
             },
             ToMandalam = new MandalamDto
             {
                 Id = entity.ToMandalamId,
                 Name = entity.ToMandalam?.Name
             },
             ToPanchayat = new PanchayatDto
             {
                 Id = entity.ToPanchayatId,
                 Name = entity.ToPanchayat?.Name
             },
             FromState = new StateDto
             {
                 Id = entity.FromStateId,
                 Name = entity.FromState?.Name
             },
             FromArea = new AreaDto
             {
                 Id = entity.FromAreaId,
                 Name = entity.FromArea?.Name
             },
             FromDistrict = new DistrictDto
             {
                 Id = entity.FromDistrictId,
                 Name = entity.FromDistrict?.Name
             },
             FromMandalam = new MandalamDto
             {
                 Id = entity.FromMandalamId,
                 Name = entity.FromMandalam?.Name
             },
             FromPanchayat = new PanchayatDto
             {
                 Id = entity.FromPanchayatId,
                 Name = entity.FromPanchayat?.Name
             },
             Reason = entity.Reason,
             JustificationComment = entity.JustificationComment,
             SubmittedDate = entity.SubmittedDate,
             SubmittedBy = entity.SubmittedBy,
             ActionDate = entity.ActionDate,
             ActionBy = entity.ActionBy,
             Status = (int)entity.Status,
             Location = entity.FromState?.Prefix + " to " + entity.ToState?.Prefix
         };
}