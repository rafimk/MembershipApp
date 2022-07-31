using Microsoft.Extensions.DependencyInjection;

namespace Membership.Shared;

public interface IMessagingConfiguration
{
    IServiceCollection Services { get; }
}

internal sealed record MessagingConfiguration(IServiceCollection Services) : IMessagingConfiguration;