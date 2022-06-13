using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;

namespace Membership.Application.Queries.Nationalities.Panchayats;

public class GetPanchayatByMandalamId : IQuery<IEnumerable<PanchayatDto>>
{
    public Guid MandalamId { get; set; }
}