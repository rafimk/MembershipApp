using Membership.Application.DTO.Security;

namespace Membership.Application.Security;

public interface IAuthenticator
{
    JwtDto CreateToken(Guid userId, string role);
    JwtDto CreateToken(Guid userId, string role, bool isAddMember, bool isAddUser);
}