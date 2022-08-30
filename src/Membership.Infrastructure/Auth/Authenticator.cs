using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Membership.Application.DTO.Security;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Membership.Application.Security;
using Membership.Core.Abstractions;
using Membership.Core.Repositories.Memberships;

namespace Membership.Infrastructure.Auth;

internal sealed class Authenticator : IAuthenticator
{
    private readonly IClock _clock;
    private readonly string _issuer;
    private readonly TimeSpan _expiry;
    private readonly string _audience;
    private readonly SigningCredentials _signingCredentials;
    private readonly JwtSecurityTokenHandler _jwtSecurityToken = new JwtSecurityTokenHandler();

    public Authenticator(IOptions<AuthOptions> options, IClock clock)
    {
        _clock = clock;
        _issuer = options.Value.Issuer;
        _audience = options.Value.Audience;
        _expiry = TimeSpan.FromHours(3);
        _signingCredentials = new SigningCredentials(new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(options.Value.SigningKey)),
                SecurityAlgorithms.HmacSha256);
    }
    
    public JwtDto CreateToken(Guid userId, string role)
    {
        var now = _clock.Current();

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
            new(ClaimTypes.Role, role)
        };

        var expires = now.Add(_expiry);
        var jwt = new JwtSecurityToken(_issuer, _audience, claims, now, expires, _signingCredentials);
        var token = _jwtSecurityToken.WriteToken(jwt);

        return new JwtDto
        {
            AccessToken = token
        };
    }
    
    public JwtDto CreateToken(Guid userId, string role, bool isAddMember, bool isAddUser)
    {
        var now = _clock.Current();

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
            new(ClaimTypes.Role, role),
            new("IsAddMember", isAddMember ? "true" : "false"),
            new("IsAddUser", isAddUser ? "true" : "false")
        };
        
        var expires = now.Add(_expiry);
        var jwt = new JwtSecurityToken(_issuer, _audience, claims, now, expires, _signingCredentials);
        var token = _jwtSecurityToken.WriteToken(jwt);

        return new JwtDto
        {
            AccessToken = token
        };
    }
}