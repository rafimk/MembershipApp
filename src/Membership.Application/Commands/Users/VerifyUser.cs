using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Users;

public record VerifyUser : ICommand
{
    public Guid UserId { get; set; }
    public string VerifyCode { get; set; }
    public string MobileLastFourDigit { get; set; }
}