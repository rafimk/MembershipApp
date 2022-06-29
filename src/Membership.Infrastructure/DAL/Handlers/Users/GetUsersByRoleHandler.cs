using Membership.Application.Abstractions;
using Membership.Application.DTO.Users;
using Membership.Application.Queries.Users;
using Membership.Core.DomainServices.Users;
using Membership.Core.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Users;

internal sealed class GetUsersByRoleHandler : IQueryHandler<GetUsersByRole, IEnumerable<UserDto>>
{
    private readonly MembershipDbContext _dbContext;
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;

    public GetUsersByRoleHandler(MembershipDbContext dbContext, IUserRepository userRepository, IUserService userService)
    {
        _dbContext = dbContext;
        _userRepository = userRepository;
        _userService = userService;
    }

    public async Task<IEnumerable<UserDto>> HandleAsync(GetUsersByRole query)
    {
        var user = await _userRepository.GetByIdAsync(query.UserId);

        var applicableUserRoles = await _userService.GetApplicableUserRolesAsync(user.Role, query.UserId);

        var availableUsers = await _dbContext.Users
            .OrderBy(x => x.FullName)
            .AsNoTracking()
            .Select(x => x.AsDto())
            .ToListAsync();
        
        

        return availableUsers.Where(x => applicableUserRoles.Contains(x.Role)).ToList();
    }
}