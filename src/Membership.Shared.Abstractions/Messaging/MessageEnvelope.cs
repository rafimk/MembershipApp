namespace Membership.Shared.Abstractions.Messaging;

public record MessageEnvelope<T>(T Message, string CorrelationId) where T : IMessage;