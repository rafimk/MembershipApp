using Membership.Core.ValueObjects;

namespace Membership.Core.Entities.Memberships.MembershipPeriods;

public class MembershipPeriod 
{
    public GenericId Id { get; private set; }
    public Date Start { get; private set; }
    public Date End {get; private set;}
    public DateTime? RegistrationStarted  { get; private set; }
    public DateTime? RegistrationEnded  { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsEnrollActive  { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private MembershipPeriod(GenericId id, Date start, Date end, DateTime? registrationStarted, 
        DateTime? registrationEnded, bool isEnrollActive, bool isActive, DateTime createdAt)
    {
        Id = id;
        Start = start;
        End = end;
        RegistrationStarted = registrationStarted;
        RegistrationEnded = registrationEnded;
        IsEnrollActive = isEnrollActive;
        IsActive = isActive;
        CreatedAt = createdAt;
    }

    private MembershipPeriod()
    {
    }
    
    public static MembershipPeriod Create(GenericId id, Date start, Date end, DateTime createdAt)
        => new(id, start, end, null, null, false, false, createdAt);

    public void Update(Date start, Date end)
    {
        Start = start;
        End = end;
    }
    
    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void EnrollActivate(DateTime startedAt)
    {
        RegistrationStarted = startedAt;
        IsEnrollActive = true;
    }

    public void EnrollDeactivate(DateTime endedAt)
    {
        RegistrationEnded = endedAt;
        IsEnrollActive = false;
    }
}