using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;

namespace Membership.Application.Queries.Memberships.RegisteredOrganizations;

public class GetRegisteredOrganizationById : IQuery<RegisteredOrganizationDto>
{
    public Guid RegisteredOrganizationId { get; set; }
}