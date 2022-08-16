using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Membership.Infrastructure.Reports;

internal static class Extensions
{
    public static IServiceCollection AddReports(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .Configure<ReportsOptions>(configuration.GetRequiredSection("reports-service"))
            .AddScoped<IReportService, ReportService>();

        return services;
    }
}