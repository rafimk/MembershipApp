using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;

namespace Membership.Application.Queries.Nationalities.Areas;

public class GetAreaByStateId : IQuery<IEnumerable<AreatDto>>
{
    public Guid StateId { get; set; }
}