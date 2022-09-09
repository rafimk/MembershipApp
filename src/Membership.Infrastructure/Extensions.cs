﻿using System.Runtime.CompilerServices;
using Membership.Application.Abstractions;
using Membership.Core.Abstractions;
using Membership.Core.DomainServices.Users;
using Membership.Infrastructure.Auth;
using Membership.Infrastructure.DAL;
using Membership.Infrastructure.DAL.Services;
using Membership.Infrastructure.Exceptions;
using Membership.Infrastructure.FileManagement;
using Membership.Infrastructure.Logging;
using Membership.Infrastructure.OCR;
using Membership.Infrastructure.Reports;
using Membership.Infrastructure.Security;
using Membership.Infrastructure.Time;
using Membership.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Polly;
using Polly.Extensions.Http;

[assembly: InternalsVisibleTo("Membership.Tests.Integration")]
[assembly: InternalsVisibleTo("Membership.Tests.Unit")]

namespace Membership.Infrastructure;
public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.Configure<AppOptions>(configuration.GetRequiredSection("app"));
        services.Configure<FileUploadOptions>(configuration.GetRequiredSection("file"));
        services.AddSingleton<ExceptionMiddleware>();
        services.AddHttpContextAccessor();
        services.AddMessaging(configuration);
        
        services.AddHttpClient<IReportService, ReportService>()
            .SetHandlerLifetime(TimeSpan.FromMinutes(5))  //Set lifetime to five minutes
            .AddPolicyHandler(GetRetryPolicy())
            .AddPolicyHandler(GetCircuitBreakerPolicy());
        
        services.AddReports(configuration);
        services.AddOcr();
        services.AddFileManagement();

        services
            .AddPostgres(configuration)
            .AddSingleton<IClock, Clock>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<IOcrService, OcrService>();

        services.AddCustomLogging();
        services.AddSecurity();
        services.AddEndpointsApiExplorer();
       
        services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "Membership API", Version = "v1" });
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
            });
        });
        
        var infrastructureAssembly = typeof(AppOptions).Assembly;
        
        services.Scan(s => s.FromAssemblies(infrastructureAssembly)
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.AddAuth(configuration);

        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseSwagger();
        app.UseSwaggerUI();
        // app.UseReDoc(reDoc =>
        // {
        //     reDoc.RoutePrefix = "docs";
        //     reDoc.SpecUrl("/swagger/v1/swagger.json");
        //     reDoc.DocumentTitle = "Membership API";
        // });
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        
        return app;
    }

    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = configuration.GetRequiredSection(sectionName);
        section.Bind(options);

        return options;
    }
    
    public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,
                retryAttempt)));
    }

    public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(2, TimeSpan.FromSeconds(60));
    }
}