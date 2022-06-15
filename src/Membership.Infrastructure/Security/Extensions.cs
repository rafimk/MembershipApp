using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Membership.Application.Security;
using Membership.Core.Entities.Users;

namespace Membership.Infrastructure.Security;

internal static class Extensions
{
    public static IServiceCollection AddSecurity(this IServiceCollection services)
    {
        services
            .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
            .AddSingleton<IPasswordManager, PasswordManager>();

        return services;
    }
}