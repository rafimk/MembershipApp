using Membership.Infrastructure.DAL.ReadModels;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL;

internal sealed class MembershipReadDbContext : DbContext
{
    public DbSet<DistrictReadModel> Districts { get; set; }
    public DbSet<MandalamReadModel> Mandalams { get; set; }
    public DbSet<PanchayatReadModel> Panchayats { get; set; }
    public DbSet<MemberReadModel> Members { get; set; }
    public DbSet<AreaReadModel> Areas { get; set; }
    public DbSet<StateReadModel> States { get; set; }
    public DbSet<UserReadModel> Users { get; set; }
    public DbSet<DisputeRequestReadModel> DisputeRequests { get; set; }

    public MembershipReadDbContext(DbContextOptions<MembershipReadDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}