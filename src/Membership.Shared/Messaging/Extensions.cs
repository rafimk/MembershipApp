using Microsoft.Extensions.DependencyInjection;

namespace Membership.Shared.Messaging;

public static class Extensions
{
    public static IServiceCollection AddMessaging(this IServiceCollection services)
        => services
            .AddSingleton<IMessagePublisher, DefaultMessagePublisher>()
            .AddSingleton<IMessageSubscriber, DefaultMessageSubscriber>();
}