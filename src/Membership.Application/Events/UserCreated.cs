namespace Membership.Application.Events;

internal record UserCreated(string Email, string UserName, string VerifyOTP) : IMessage;