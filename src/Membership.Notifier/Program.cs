using Membership.Notifier.Services;
using Membership.Shared.Messaging;
using Membership.Shared.Observability;
using Membership.Shared.Pulsar;
using Membership.Shared.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IMailService, Services.MailService>();

builder.Services
    .AddHttpContextAccessor()
    .AddSerialization()
    .AddMessaging()
    .AddPulsar()
    .AddHostedService<NotifierMessagingBackgroundService>();

var app = builder.Build();
app.UseCorrelationId();

app.MapGet("/", () => "Membership - Notifier");

app.Run();
