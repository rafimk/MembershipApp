using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.DTO.Widgets;
using Membership.Application.Queries.Widgets;
using Membership.Core.DomainServices.Users;
using Membership.Core.Repositories.Users;
using Membership.Core.ValueObjects;
using Membership.Infrastructure.DAL.Exceptions;
using Membership.Infrastructure.DAL.Handlers.Users.Policies;
using Membership.Infrastructure.DAL.Handlers.Widgets.Policies;
using Microsoft.AspNetCore.Identity;
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

        List<WidgetDto> widget = new();

        List<WidgetDetailDto> widgetDetails= new();
        
        if (user.Role == UserRole.CentralCommitteeAdmin())
        {
            var userCountByState = await _dbContext.Users
                .Include(x => x.State)
                .Where(x => x.Role == UserRole.StateAdmin())
                .GroupBy(x => x.StateId)
                .Select(x => new { StateId = x.Key, StateName = x.First().State.Name, Count = x.Count() })
                .ToListAsync();
            
            Int32 totalUserCount = 0; 
            
            foreach(var item in userCountByState)
            {
                if (item.StateName is not null)
                {
                    totalUserCount += item.Count;
                    widgetDetails.Add(new WidgetDetailDto
                    {
                        Text = item.StateName,
                        IntValue = item.Count,
                        TextValue = null
                    });
                }
            }
            
            widget.Add(new WidgetDto
            {
                No = 1,
                Title = "Total Users",
                SummaryValue = totalUserCount,
                SummaryText = null,
                Details = widgetDetails
            });

            return widget;
        }
        
        if (user.Role == UserRole.StateAdmin())
        {
            var userCountByDistrict = await _dbContext.Users
                .Include(x => x.District)
                .Where(x => x.Role == UserRole.DistrictAdmin() || x.Role == UserRole.DistrictAgent())
                .GroupBy(x => x.DistrictId)
                .Select(x => new { DistrictId = x.Key, DistrictName = x.First().District.Name, Count = x.Count() })
                .ToListAsync();
            
            Int32 totalUserCount = 0; 
            
            foreach(var item in userCountByDistrict)
            {
                if (item.DistrictName is not null)
                {
                    totalUserCount += item.Count;
                    widgetDetails.Add(new WidgetDetailDto
                    {
                        Text = item.DistrictName,
                        IntValue = item.Count,
                        TextValue = null
                    });
                }
            }
            
            widget.Add(new WidgetDto
            {
                No = 1,
                Title = "Total Users",
                SummaryValue = totalUserCount,
                SummaryText = null,
                Details = widgetDetails
            });

            return widget;
        }
        
        if (user.Role == UserRole.DistrictAdmin())
        {
            var userCountByMandalam = await _dbContext.Users
                .Include(x => x.Mandalam)
                .Where(x => x.Role == UserRole.MandalamAgent())
                .GroupBy(x => x.MandalamId)
                .Select(x => new { MandalamId = x.Key, MandalamName = x.First().Mandalam.Name, Count = x.Count() })
                .ToListAsync();
            
            Int32 totalUserCount = 0; 
            
            foreach(var item in userCountByMandalam)
            {
                if (item.MandalamName is not null)
                {
                    totalUserCount += item.Count;
                    widgetDetails.Add(new WidgetDetailDto
                    {
                        Text = item.MandalamName,
                        IntValue = item.Count,
                        TextValue = null
                    });
                }
            }
            
            widget.Add(new WidgetDto
            {
                No = 1,
                Title = "Total Users",
                SummaryValue = totalUserCount,
                SummaryText = null,
                Details = widgetDetails
            });

            return widget;
        }

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