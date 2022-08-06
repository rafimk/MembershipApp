﻿using Humanizer;
using Membership.Application.DTO.Memberships;
using Membership.Application.DTO.Users;
using Membership.Application.DTO.Widgets;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Widgets.Policies;

internal sealed class DistrictAdminWidgetPolicy : IWidgetPolicy
{
    public bool CanBeApplied(UserRole currentUserRole)
        => currentUserRole == UserRole.DistrictAdmin();

    public IEnumerable<WidgetDto> GetData(UserRole currentUserRole, 
        IEnumerable<UserDto> users, 
        IEnumerable<MemberDto> members)
    {
        List<WidgetDto> widget = new();

        List<WidgetDetailDto> widgetDetails= new();

        if (users.Any())
        {

            var userResult = users.GroupBy(x => x.Role.ToString()).OrderBy(stu => stu.Key);

            foreach (var group in userResult)
            {
                widgetDetails.Add(new WidgetDetailDto
                {
                    Text = group.Key.Humanize(LetterCasing.Title),
                    IntValue = group.Count(),
                    TextValue = ""
                });
            }

            // Users
            widget.Add(new WidgetDto
            {
                No = 1,
                Title = "Total Users",
                SummaryValue = users.Count(),
                SummaryText = null,
                Details = widgetDetails
            });
        }

        return widget;
    }
}