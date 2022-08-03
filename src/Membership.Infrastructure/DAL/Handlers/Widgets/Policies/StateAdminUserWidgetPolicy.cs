﻿using Membership.Application.DTO.Memberships;
using Membership.Application.DTO.Users;
using Membership.Application.DTO.Widgets;
using Membership.Core.ValueObjects;

namespace Membership.Infrastructure.DAL.Handlers.Widgets.Policies;

internal sealed class StateAdminUserWidgetPolicy : IWidgetPolicy
{
    public bool CanBeApplied(UserRole currentUserRole)
        => currentUserRole == UserRole.StateAdmin();

    public IEnumerable<WidgetDto> GetData(UserRole currentUserRole, 
        IEnumerable<UserDto> users, 
        IEnumerable<MemberDto> members)
    {
        return null;
    }
}