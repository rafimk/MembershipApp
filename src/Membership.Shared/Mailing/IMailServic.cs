namespace Membership.Shared.Mailing;

public interface IMailService
{
    Task SendEmailAsync(MailRequest mailRequest);
}