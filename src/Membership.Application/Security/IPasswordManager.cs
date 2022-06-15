namespace Membership.Application.Security;

public interface IPasswordManager
{
    string Secure(string password);
    string Generate();
    bool Validate(string password, string securedPassword);
}