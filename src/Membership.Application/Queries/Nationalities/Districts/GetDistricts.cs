using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;

namespace Membership.Application.Queries.Nationalities.Districts;

public class GetDistricts : IQuery<IEnumerable<DistrictDto>>
{
}