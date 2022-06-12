using Microsoft.Extensions.DependencyInjection;

namespace Membership.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        // services.AddSingleton<IReservationPolicy, RegularEmployeeReservationPolicy>();
        // services.AddSingleton<IParkingReservationService, ParkingReservationService>();
        
        return services;
    }
}