using Membership.Core.Policies.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Membership.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddSingleton<IUserCreatePolicy, CentralCommitteeUserCreatePolicy>();
        services.AddSingleton<IUserCreatePolicy, DistrictAdminUserCreatePolicy>();
        services.AddSingleton<IUserCreatePolicy, StateAdminUserCreatePolicy>();
       
        return services;
    }
}