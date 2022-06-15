using Membership.Application.Abstractions;
using Membership.Application.DTO.Users;

namespace Membership.Application.Queries.Users;

public class GetUserByMobile : IQuery<UserDto>
{
    public string UserMobile { get; set; }
}