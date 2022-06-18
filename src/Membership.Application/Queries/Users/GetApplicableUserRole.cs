using Membership.Application.Abstractions;
using Membership.Application.DTO.Users;

namespace Membership.Application.Queries.Users;

public class GetApplicableUserRole : IQuery<IEnumerable<string>>
{
    public Guid? UserId { get; set; }
}