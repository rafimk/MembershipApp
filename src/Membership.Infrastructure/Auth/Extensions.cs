using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Membership.Application.Security;

namespace Membership.Infrastructure.Auth;

internal static class Extensions
{
    private const string OptionsSectionName = "auth";
    
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetOptions<AuthOptions>(OptionsSectionName);

        services
            .Configure<AuthOptions>(configuration.GetRequiredSection(OptionsSectionName))
            .AddSingleton<IAuthenticator, Authenticator>()
            .AddSingleton<ITokenStorage, HttpContextTokenStorage>()
            .AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.Audience = options.Audience;
                o.IncludeErrorDetails = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = options.Issuer,
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SigningKey))
                };
            });

        services.AddAuthorization(authorization =>
        {
            authorization.AddPolicy("is-centralcommittee-admin", policy =>
            {
                policy.RequireRole("centralcommittee-admin");
            });

            authorization.AddPolicy("is-state-admin", policy =>
            {
                policy.RequireRole("state-admin");
            });

            authorization.AddPolicy("is-district-admin", policy =>
            {
                policy.RequireRole("district-admin");
            });

            authorization.AddPolicy("is-agent", policy =>
            {
                policy.RequireRole("mandalam-agent");
            });

            authorization.AddPolicy("is-agent", policy =>
            {
                policy.RequireRole("district-agent");
            });
        });

        return services;
    }
}