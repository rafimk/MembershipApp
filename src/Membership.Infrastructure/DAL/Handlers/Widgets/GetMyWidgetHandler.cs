using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.DTO.Widgets;
using Membership.Application.Queries.Widgets;
using Membership.Core.DomainServices.Users;
using Membership.Core.Repositories.Users;
using Membership.Infrastructure.DAL.Exceptions;
using Membership.Infrastructure.DAL.Handlers.Users.Policies;
using Membership.Infrastructure.DAL.Handlers.Widgets.Policies;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Widgets;

internal sealed class GetMyWidgetHandler : IQueryHandler<GetMyWidget, IEnumerable<WidgetDto>>
{
    private readonly MembershipDbContext _dbContext;
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;
    private readonly IEnumerable<IWidgetPolicy> _widgetPolicies;
    private readonly IEnumerable<IUserDataRetrievePolicy> _userDataRetrievepolicies;

    public GetMyWidgetHandler(MembershipDbContext dbContext,
        IUserRepository userRepository, 
        IUserService userService,
        IEnumerable<IWidgetPolicy> widgetPolicies,
        IEnumerable<IUserDataRetrievePolicy> userDataRetrievepolicies)
    {
        _dbContext = dbContext;
        _userRepository = userRepository;
        _userService = userService;
        _widgetPolicies = widgetPolicies;
        _userDataRetrievepolicies = userDataRetrievepolicies;
    }

    public async Task<IEnumerable<WidgetDto>> HandleAsync(GetMyWidget query)
    {
        var user = await _userRepository.GetByIdAsync((Guid)query.UserId);
        
        var applicableUserRoles = await _userService.GetApplicableUserRolesAsync(user.Role, query.UserId);

        var availableUsers = await _dbContext.Users
            .OrderBy(x => x.FullName)
            .AsNoTracking()
            .Select(x => x.AsDto())
            .ToListAsync();
        
        var userDataRetrievepolicies = _userDataRetrievepolicies.SingleOrDefault(x => x.CanBeApplied(user.Role));

        if (userDataRetrievepolicies is null)
        {
            throw new NoUserDataReteivePolicyFoundException(user.Role.ToString());
        }
        
        var users = userDataRetrievepolicies.GetData(user.Role, availableUsers, applicableUserRoles, user.StateId, user.DistrictId);

        var widgetPolicies = _widgetPolicies.SingleOrDefault(x => x.CanBeApplied(user.Role));

        if (widgetPolicies is null)
        {
            throw new NoWidgetPolicyFoundException(user.Role.ToString());
        }

        return widgetPolicies.GetData(user.Role, users, new List<MemberDto>());
    }
}