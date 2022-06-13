using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;

namespace Membership.Application.Queries.Nationalities.Areas;

public class GetAreaById : IQuery<AreaDto>
{
    public Guid AreaId { get; set; }
}