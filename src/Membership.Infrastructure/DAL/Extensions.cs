using Membership.Application.Abstractions;
using Membership.Core.Repositories.Commons;
using Membership.Core.Repositories.Memberships;
using Membership.Core.Repositories.Nationalities;
using Membership.Core.Repositories.Users;
using Membership.Infrastructure.DAL.Decorators;
using Membership.Infrastructure.DAL.Repositories.Commons;
using Membership.Infrastructure.DAL.Repositories.Memberships;
using Membership.Infrastructure.DAL.Repositories.Nationalities;
using Membership.Infrastructure.DAL.Repositories.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Membership.Infrastructure.DAL;

internal static class Extensions
{
    private const string OptionsSectionName = "postgres";
    
    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PostgresOptions>(configuration.GetRequiredSection(OptionsSectionName));
        var postgresOptions = configuration.GetOptions<PostgresOptions>(OptionsSectionName);
        services.AddDbContext<MembershipDbContext>(x => x.UseNpgsql(postgresOptions.ConnectionString));
        
        services.AddScoped<IFileAttachmentRepository, PostgresFileAttachmentRepository>();
        services.AddScoped<IMemberRepository, PostgresMemberRepository>();
        services.AddScoped<IMembershipPeriodRepository, PostgresMembershipPeriodRepository>();
        services.AddScoped<IProfessionRepository, PostgresProfessionRepository>();
        services.AddScoped<IQualificationRepository, PostgresQualificationRepository>();
        
        services.AddScoped<IAreaRepository, PostgresAreaRepository>();
        services.AddScoped<IDistrictRepository, PostgresDistrictRepository>();
        services.AddScoped<IStateRepository, PostgresStateRepository>();
        services.AddScoped<IMandalamRepository, PostgresMandalamRepository>();
        services.AddScoped<IPanchayatRepository, PostgresPanchayatRepository>();
        
        services.AddScoped<IUserRepository, PostgresUserRepository>();
       
        services.AddHostedService<DatabaseInitializer>();
        services.AddScoped<IUnitOfWork, PostgresUnitOfWork>();

        services.TryDecorate(typeof(ICommandHandler<>), typeof(UnitOfWorkCommandHandlerDecorator<>));

        // EF Core + Npgsql issue
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        
        return services;
    }
}