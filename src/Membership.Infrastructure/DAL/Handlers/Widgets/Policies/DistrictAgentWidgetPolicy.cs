using Membership.Application.DTO.Memberships;
using Membership.Application.DTO.Users;
using Membership.Application.DTO.Widgets;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Widgets.Policies;

internal sealed class DistrictAgentWidgetPolicy : IWidgetPolicy
{
    public bool CanBeApplied(UserRole currentUserRole)
        => currentUserRole == UserRole.DistrictAgent();

    public IEnumerable<WidgetDto> GetData(UserRole currentUserRole,
        IEnumerable<UserDto> users, 
        IEnumerable<MemberDto> members)
    {
        List<WidgetDto> widget = new();

        List<WidgetDetailDto> widgetDetails= new();

        return widget;
    }
}