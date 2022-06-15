using Membership.Application.DTO.Security;

namespace Membership.Application.Security;

public interface ITokenStorage
{
    void Set(JwtDto jwt);
    JwtDto Get();
}