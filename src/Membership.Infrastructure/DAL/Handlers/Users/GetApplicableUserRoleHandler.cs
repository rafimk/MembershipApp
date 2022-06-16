using Membership.Application.Abstractions;
using Membership.Application.DTO.Users;
using Membership.Application.Queries.Users;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Users;

internal sealed class GetApplicableUserRoleHandler : IQueryHandler<GetApplicableUserRoleHandler, UserDto>
{
    private readonly IUserCreatePolicy _userCreatePolicy;
    
    public GetApplicableUserRoleHandler(IUserCreatePolicy userCreatePolicy)
        => _userCreatePolicy = userCreatePolicy;
    
    public async Task<IEnumerable<string>> HandleAsync(GetApplicableUserRoleHandler query)
    {
        var currentUserRole = userRole;
        if (!_userCreatePolicy.CanBeAppliedcurrentUserRole)
        {
            return null;
        }

        return _userCreatePolicy.PermittedUserRole(currentUserRole);
    }
}