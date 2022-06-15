namespace Membership.Shared.Abstractions.Mailing;

public interface IMailService
{
    Task SendEmailAsync(IMailRequest mailRequest);
}