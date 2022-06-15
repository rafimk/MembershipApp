using Microsoft.Extensions.DependencyInjection;
using Membership.Shared.Abstractions.Serialization;

namespace Membership.Shared.Serialization;

public static class Extensions
{
    public static IServiceCollection AddSerialization(this IServiceCollection services)
        => services.AddSingleton<ISerializer, SystemTextJsonSerializer>();
}