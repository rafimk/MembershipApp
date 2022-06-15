using Membership.Application.Abstractions;
using Membership.Application.DTO.Users;

namespace Membership.Application.Queries.Users;

public class GetUsers : IQuery<IEnumerable<UserDto>>
{
}