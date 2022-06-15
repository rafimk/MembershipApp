using Membership.Shared.Abstractions.Messaging;

namespace Membership.Shared.Messaging;

internal sealed class DefaultMessageSubscriber : IMessageSubscriber
{
    public Task SubscribeAsync<T>(string topic, Action<MessageEnvelope<T>> handler) where T : class, IMessage => Task.CompletedTask;
}