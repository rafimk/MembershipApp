using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;

namespace Membership.Application.Queries.Nationalities.Areas;

public class GetAreaByStateId : IQuery<IEnumerable<AreaDto>>
{
    public Guid StateId { get; set; }
}