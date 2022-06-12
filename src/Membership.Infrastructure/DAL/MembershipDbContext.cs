using Membership.Core.Entities.Nationalities;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL;

internal sealed class MembershipDbContext : DbContext
{
    public DbSet<State> States { get; set; }

    public MembershipDbContext(DbContextOptions<MembershipDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}