using Membership.Shared;

namespace Membership.Application.Messages;

public record UserCreated(string Name, string Email, string Otp) :  IMessage;