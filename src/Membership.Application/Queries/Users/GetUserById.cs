using Membership.Application.Abstractions;
using Membership.Application.DTO.Users;

namespace Membership.Application.Queries.Users;

public class GetUserById : IQuery<UserDto>
{
    public Guid UserId { get; set; }
}