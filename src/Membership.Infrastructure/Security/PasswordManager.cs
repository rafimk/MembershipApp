using Membership.Application.Security;
using Membership.Core.Entities.Users;
using Microsoft.AspNetCore.Identity;
using PasswordGenerator;

namespace Membership.Infrastructure.Security;

internal sealed class PasswordManager : IPasswordManager
{
    private readonly IPasswordHasher<User> _passwordHasher;

    public PasswordManager(IPasswordHasher<User> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public string Secure(string password) => _passwordHasher.HashPassword(default, password);

    public string Generate()
    {
        var pwd = new Password(8).IncludeLowercase().IncludeUppercase().IncludeNumeric();
        return pwd.Next();
    }

    public string GenerateSimple()
    {
        var pwdPrefix = new Password().IncludeUppercase().LengthRequired(2);
        var pwdSuffix = new Password().IncludeNumeric().LengthRequired(6);
        RandomGenerator randomGenerator = new RandomGenerator();
        var prefix = "";
        do 
        {
            prefix = randomGenerator.RandomString(2);
        } while (prefix.Contains("I") && prefix.Contains("O"));
        var password = randomGenerator.RandomString(2, false) + pwdSuffix.Next();
        return password;
    }

    public bool Validate(string password, string securedPassword)
        => _passwordHasher.VerifyHashedPassword(default, securedPassword, password) ==
           PasswordVerificationResult.Success;
}