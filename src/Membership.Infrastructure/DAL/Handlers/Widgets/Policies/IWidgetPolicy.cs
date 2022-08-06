using Membership.Application.DTO.Memberships;
using Membership.Application.DTO.Users;
using Membership.Application.DTO.Widgets;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Widgets.Policies;

public interface IWidgetPolicy
{
    bool CanBeApplied(UserRole currentUserRole);
    
    IEnumerable<WidgetDto> GetData(UserRole currentUserRole, 
        IEnumerable<UserDto> users, 
        IEnumerable<MemberDto> members);
}