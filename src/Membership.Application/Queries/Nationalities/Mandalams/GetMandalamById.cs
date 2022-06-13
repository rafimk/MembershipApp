using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;

namespace Membership.Application.Queries.Nationalities.Mandalams;

public class GetMandalamById : IQuery<MandalamDto>
{
    public Guid MandalamId { get; set; }
}