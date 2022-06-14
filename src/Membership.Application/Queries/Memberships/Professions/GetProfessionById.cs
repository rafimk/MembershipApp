using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;

namespace Membership.Application.Queries.Memberships.Professions;

public class GetProfessionById : IQuery<ProfessionDto>
{
    public Guid ProfessionsId { get; set; }
}