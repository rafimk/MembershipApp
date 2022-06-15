namespace Membership.Shared.Abstractions.Mailing;

public interface IMailRequest
{
    public string ToEmail { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}