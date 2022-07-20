using System.Runtime.CompilerServices;
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
using Membership.Infrastructure.OCR.Policies;
using Membership.Infrastructure.Security;
using Membership.Infrastructure.Time;
using Membership.Shared.Messaging;
using Membership.Shared.Pulsar;
using Membership.Shared.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

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
        services.AddSerialization();
        services.AddMessaging();
        services.AddPulsar();

        services
            .AddPostgres(configuration)
            .AddSingleton<IClock, Clock>()
            .AddSingleton<ICardReadPolicy, NewCardFrontSideReadPolicy>()
            .AddSingleton<ICardReadPolicy, NewCardBackSideReadPolicy>()
            .AddSingleton<ICardReadPolicy, OldCardFrontSideReadPolicy>()
            .AddSingleton<ICardReadPolicy, OldCardBackSideReadPolicy>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<IBufferedFileUploadService, BufferedFileUploadLocalService>()
            
            .AddScoped<IOcrService, OcrService>();

        services.AddCustomLogging();
        services.AddSecurity();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(swagger =>
        {
            swagger.EnableAnnotations();
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Membership API",
                Version = "v1"
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
}