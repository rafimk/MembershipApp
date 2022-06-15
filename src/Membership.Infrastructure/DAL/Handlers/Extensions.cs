using Membership.Application.DTO.Nationalities;
using Membership.Application.DTO.Users;
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
    
    public static AreaDto AsDto(this Area entity)
        => new()
        {
            Id = entity.Id.Value,
            Name = entity.Name,
            State = new StateDto
            {
                Id = entity.State.Id.Value,
                Name = entity.State.Name,
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
            VerifiedAt = entity.VerifiedAt
        };
}