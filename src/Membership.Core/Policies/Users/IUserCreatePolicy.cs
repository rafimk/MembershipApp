namespace MMS.Domain.Policies.Users;

public interface IUserCreatePolicy
{
    bool CanBeApplied(UserRole currentUserRole);
    UserRole PermittedUserRole(UserRole currentUserRole);
}