using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;

namespace Membership.Application.Queries.Memberships.WelfareSchemes;

public class GetWelfareSchemeById : IQuery<WelfareSchemeDto>
{
    public Guid WelfareSchemeId { get; set; }
}