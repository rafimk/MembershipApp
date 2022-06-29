using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;

namespace Membership.Application.Queries.Memberships.RegisteredOrganizations;

public class GetRegisteredOrganizations : IQuery<IEnumerable<RegisteredOrganizationDto>>
{
}