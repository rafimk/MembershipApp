namespace Membership.Application.Security;

public interface IPasswordManager
{
    string Secure(string password);
    string Generate();
    string GenerateSimple();
    bool Validate(string password, string securedPassword);
}