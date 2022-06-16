using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;

namespace Membership.Application.Queries.Memberships.Professions;

public class GetProfessionById : IQuery<ProfessionDto>
{
    public Guid ProfessionId { get; set; }
}