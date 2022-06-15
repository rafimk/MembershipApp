using Membership.Shared.Abstractions.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Membership.Shared.Pulsar;

public static class Extensions
{
    public static IServiceCollection AddPulsar(this IServiceCollection services)
        => services
            .AddSingleton<IMessagePublisher, PulsarMessagePublisher>()
            .AddSingleton<IMessageSubscriber, PulsarMessageSubscriber>();
}