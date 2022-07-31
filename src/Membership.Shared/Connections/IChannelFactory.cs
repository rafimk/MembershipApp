using RabbitMQ.Client;

namespace Membership.Shared.Connections;

public interface IChannelFactory
{
    IModel Create();
}