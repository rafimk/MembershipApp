namespace MMS.Domain.Policies.Users;

internal sealed class StateAdminUserCreatePolicy : IUserCreatePolicy
{
   
    public bool CanBeApplied(UserRole currentUserRole)
        => currentUserRole == UserRole.StateAdmin;

    public UserRole PermittedUserRole(UserRole currentUserRole)
    {
        return UserRole.DistrictAdmin;
    }
}