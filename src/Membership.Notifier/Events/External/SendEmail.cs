using Membership.Shared.Messaging;

namespace Membership.Notifier.Events.External;

internal record SendEmail(string Email, string UserName, string VerifyOTP) : IMessage