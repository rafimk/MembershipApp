using Membership.Shared.Accessors;
using Membership.Shared.Connections;
using Membership.Shared.Dispatchers;
using Membership.Shared.Publishers;
using Membership.Shared.Subscribers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Membership.Shared;

public static class Extensions
{
    private const string OptionsSectionName = "rabbitmq";
    public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration, Action<IMessagingConfiguration> configure = default)
    {
        var rabbitmqOptions = configuration.GetOptions<RabbitmqOptions>(OptionsSectionName);
        var factory = new ConnectionFactory {HostName = rabbitmqOptions.Host};
        var connection = factory.CreateConnection();

        services.AddSingleton(connection);
        services.AddSingleton<ChannelAccessor>();
        services.AddSingleton<IChannelFactory, ChannelFactory>();
        services.AddSingleton<IMessagePublisher, MessagePublisher>();
        services.AddSingleton<IMessageSubscriber, MessageSubscriber>();
        services.AddSingleton<IMessageDispatcher, MessageDispatcher>();
        services.AddSingleton<IMessageIdAccessor, MessageIdAccessor>();

        services.Scan(cfg => cfg.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
            .AddClasses(c => c.AssignableTo(typeof(IMessageHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        configure?.Invoke(new MessagingConfiguration(services));
        
        return services;
    }
    
    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = configuration.GetRequiredSection(sectionName);
        section.Bind(options);

        return options;
    }
}