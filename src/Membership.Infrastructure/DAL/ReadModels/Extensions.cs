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
            MembershipId = entity.MembershipId,
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
            VerifiedAt = entity.VerifiedAt
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
             IsDisputeCommittee = entity.IsDisputeCommittee,
             CreatedAt = entity.CreatedAt
         };
}