using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;

namespace Membership.Application.Queries.Nationalities.Districts;

public class GetDistrictById : IQuery<DistrictDto>
{
    public Guid DistrictId { get; set; }
}