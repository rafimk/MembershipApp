using Membership.Core.Abstractions;
using Membership.Core.Entities.Nationalities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Membership.Infrastructure.DAL;

internal sealed class DatabaseInitializer : IHostedService
{
    // Service locator "anti-pattern" (but it depends) :)
    private readonly IServiceProvider _serviceProvider;
    private readonly IClock _clock;

    public DatabaseInitializer(IServiceProvider serviceProvider, IClock clock)
    {
        _serviceProvider = serviceProvider;
        _clock = clock;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<MembershipDbContext>();
        await dbContext.Database.MigrateAsync(cancellationToken);

        if (await dbContext.States.AnyAsync(cancellationToken))
        {
            return;
        }
        
        var states = new List<State>
        {
            State.Create(Guid.Parse("E50C3216-A1B5-46CF-8386-185D3BCF11CF"), "FUJAIRAH", _clock.Current()),
            State.Create(Guid.Parse("B4A729EB-E004-4E53-8C03-220D4B4F9B12"), "SHARJAH", _clock.Current()),
            State.Create(Guid.Parse("C4613305-D19A-4719-931A-58D7D5853A41"), "ABU DHABI", _clock.Current()),
            State.Create(Guid.Parse("B9BE4F9E-1EF0-4EF6-B4B3-7015F689532B"), "AL AIN", _clock.Current()),
            State.Create(Guid.Parse("D6C29ACF-2C83-446F-BFA2-70C914218969"), "DUBAI", _clock.Current()),
            State.Create(Guid.Parse("B6D27754-B617-47FA-B8CB-E23C92EC2AD0"), "UMM AL QUWAIN", _clock.Current()),
            State.Create(Guid.Parse("353BE5CA-EA67-47EA-9E78-E3E8B55A8A15"), "RAS AL KHAIMAH", _clock.Current()),
            State.Create(Guid.Parse("6C0C8AC8-8C1E-43AD-8399-F66E2D53A9C9"), "AJMAN", _clock.Current()),
           
        };

        await dbContext.States.AddRangeAsync(states, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}