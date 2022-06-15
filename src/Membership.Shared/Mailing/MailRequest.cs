using Membership.Shared.Abstractions.Mailing;

namespace Membership.Shared.Mailing;

public class MailRequest : IMailRequest
{
    public string ToEmail { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}