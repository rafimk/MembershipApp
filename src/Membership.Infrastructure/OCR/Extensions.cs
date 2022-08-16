using Membership.Infrastructure.OCR.Policies;
using Microsoft.Extensions.DependencyInjection;

namespace Membership.Infrastructure.OCR;

internal static class Extensions
{
    public static IServiceCollection AddOcr(this IServiceCollection services)
    {
        services
            .AddSingleton<ICardReadPolicy, NewCardFrontSideReadPolicy>()
            .AddSingleton<ICardReadPolicy, NewCardBackSideReadPolicy>()
            .AddSingleton<ICardReadPolicy, OldCardFrontSideReadPolicy>()
            .AddSingleton<ICardReadPolicy, OldCardBackSideReadPolicy>()
            .AddSingleton<ICardReadPolicy, OldCardBackSideChildReadPolicy>();

        return services;
    }
}