using Membership.Shared.Abstractions.Messaging;

namespace Membership.Application.Messaging;

internal sealed class DefaultMessagePublisher : IMessagePublisher
{
    public Task PublishAsync<T>(string topic, T message) where T : class, IMessage => Task.CompletedTask;
}