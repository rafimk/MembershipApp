using Membership.Application.Abstractions;
using Membership.Infrastructure.Logging.Decorators;
using Microsoft.Extensions.DependencyInjection;

namespace Membership.Infrastructure.Logging;

internal static class Extensions
{
    public static IServiceCollection AddCustomLogging(this IServiceCollection services)
    {
        services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));

        return services;
    }
}