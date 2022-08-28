namespace Membership.Core.Entities.Users;

public class UserLoginAttempt
{
    public Guid Id { get; private set; }
    public int NumberOfIncorrectLoginAttempts { get; private set; }
    public DateTime LastIncorrectLogin { get; private set; }
    public Boolean Locked { get; private set; }
    
    public UserLoginAttempt()
    {
    }

    private UserLoginAttempt(Guid id, int numberOfAttempts, DateTime lastIncorrectLogin, Boolean locked)
    {
        Id = id;
        NumberOfIncorrectLoginAttempts = numberOfAttempts;
        LastIncorrectLogin = lastIncorrectLogin;
        Locked = locked;
    }
    
    public static UserLoginAttempt Create(Guid id, int numberOfAttempts, DateTime lastIncorrectLogin, Boolean locked)
        => new(id, numberOfAttempts, lastIncorrectLogin, locked);
    
    public void IncreaseIncorrectLoginAttempts(DateTime _dateTime)
    {
        NumberOfIncorrectLoginAttempts += 1;
        if (NumberOfIncorrectLoginAttempts >= 5)
        {
            Locked = true;
        }
        LastIncorrectLogin = _dateTime;
    }
}