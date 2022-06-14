using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;

namespace Membership.Application.Queries.Users;

public class GetUserByEmail : IQuery<UserDto>
{
    public string UserEmail { get; set; }
}