using Membership.Core.Entities.Commons;
using Membership.Core.Entities.Memberships;
using Membership.Core.Entities.Memberships.Members;
using Membership.Core.Entities.Memberships.MembershipPeriod;
using Membership.Core.Entities.Memberships.Professions;
using Membership.Core.Entities.Memberships.Qualifications;
using Membership.Core.Entities.Nationalities;
using Membership.Core.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL;

internal sealed class MembershipDbContext : DbContext
{
    public DbSet<Area> Areas { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<Mandalam> Mandalams { get; set; }
    public DbSet<Panchayat> Panchayats { get; set; }
    public DbSet<State> States { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Profession> Professions { get; set; }
    public DbSet<Qualification> Qualifications { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<MembershipPeriod> MembershipPeriods { get; set; }
    public DbSet<FileAttachment> FileAttachments { get; set; }

    public MembershipDbContext(DbContextOptions<MembershipDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}