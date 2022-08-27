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

    public bool Validate(string password, string securedPassword)
        => _passwordHasher.VerifyHashedPassword(default, securedPassword, password) ==
           PasswordVerificationResult.Success;
}