﻿using System.Text;
using System.Text.Json;
using Membership.Shared.Accessors;
using Membership.Shared.Connections;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Membership.Shared.Subscribers;

internal sealed class MessageSubscriber : IMessageSubscriber
{
    private readonly IMessageIdAccessor _messageIdAccessor;
    private readonly IModel _channel;

    public MessageSubscriber(IChannelFactory channelFactory, IMessageIdAccessor messageIdAccessor)
    {
        _channel = channelFactory.Create();
        _messageIdAccessor = messageIdAccessor;
    }

    public IMessageSubscriber SubscribeMessage<TMessage>(string queue, string routingKey, string exchange, 
        Func<TMessage, BasicDeliverEventArgs, Task> handle) where TMessage : class, IMessage
    {
        _channel.ExchangeDeclare(exchange, "topic", durable: false, autoDelete: false, null);
        _channel.QueueDeclare(queue, durable: false, autoDelete: false, exclusive: false);
        _channel.QueueBind(queue, exchange, routingKey);
        
        _channel.BasicQos(0, 1, false);

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = JsonSerializer.Deserialize<TMessage>(Encoding.UTF8.GetString(body));

            _messageIdAccessor.SetMessageId(ea.BasicProperties.MessageId);
            
            await handle(message, ea);
            
            _channel.BasicAck(ea.DeliveryTag, multiple: false);
        };

        _channel.BasicConsume(queue, autoAck: false, consumer: consumer);
        
        return this;
    }
}