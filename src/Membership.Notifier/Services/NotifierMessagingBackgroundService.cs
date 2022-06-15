using Membership.Notifier.Events.External;
using Membership.Shared.Messaging;

namespace Membership.Notifier.Services;

internal sealed class NotifierMessagingBackgroundService : BackgroundService
{
    private readonly IMessageSubscriber _messageSubscriber;
    private readonly ILogger<NotifierMessagingBackgroundService> _logger;

    public NotifierMessagingBackgroundService(IMessageSubscriber messageSubscriber, 
        ILogger<NotifierMessagingBackgroundService> logger)
    {
        _messageSubscriber = messageSubscriber;
        _logger = logger;
    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _messageSubscriber.SubscribeAsync<SendEmail>("user-created", messageEnvelope =>
        {
            var correlationId = messageEnvelope.CorrelationId;
            _logger.LogInformation($"Email : '{messageEnvelope.Message.Email}' for the user: '{messageEnvelope.Message.UserName}' has been created.");
        });
        
        return Task.CompletedTask;
    }
}