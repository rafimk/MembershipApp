using Membership.Application.DTO.Nationalities;
using Membership.Core.Entities.Nationalities;

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
}