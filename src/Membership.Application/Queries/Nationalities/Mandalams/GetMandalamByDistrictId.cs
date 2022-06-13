using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;

namespace Membership.Application.Queries.Nationalities.Mandalams;

public class GetMandalamByDistrictId : IQuery<IEnumerable<MandalamDto>>
{
    public Guid DistrictId { get; set; }
}