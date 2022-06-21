using Membership.Application.Abstractions;
using Membership.Application.DTO.Users;

namespace Membership.Application.Queries.Users;

public class GetUsersByRole : IQuery<IEnumerable<UserDto>>
{
    public Guid UserId { get; set; }
}