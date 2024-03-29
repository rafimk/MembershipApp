using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Users;

public record VerifyUser : ICommand
{
    public string Email { get; set; }
    public string VerifyCode { get; set; }
    public string MobileLastFourDigit { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}