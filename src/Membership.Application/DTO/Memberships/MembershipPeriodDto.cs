namespace Membership.Application.DTO.Memberships;

public class MembershipPeriodDto
{
    public Guid Id { get; set; }
    public DateTimeOffset Start { get; set; }
    public DateTimeOffset End {get; set;}
    public DateTimeOffset? RegistrationStarted { get; set; }
    public DateTimeOffset? RegistrationEnded { get; set; }
    public bool IsActive { get; set; }
    public bool IsEnrollActive { get; set; }
    public DateTime CreatedAt { get; set; }
}