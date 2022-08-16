using Microsoft.Extensions.DependencyInjection;

namespace Membership.Infrastructure.FileManagement;

internal static class Extensions
{
    public static IServiceCollection AddFileManagement(this IServiceCollection services)
    {
        services
            .AddScoped<IBufferedFileUploadService, AzureStorageBlobsFileUploadService>();
        
        return services;
    }
}