using Membership.Core.Entities.Nationalities;
using Membership.Core.Repositories.Nationalities;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Repositories.Memberships;

internal sealed class PostgresQualificationRepository : IQualificationRepository
{
    private readonly MMSDbContext _dbContext;

    public PostgresQualificationRepository(MMSDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public  Task<Qualification> GetByIdAsync(GenericId id)
        => _dbContext.Qualifications.SingleOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<Qualification>> GetAsync()
        => await _dbContext.Qualifications.Where(x => !x.IsDeleted).ToListAsync();

    public async Task AddAsync(Qualification qualification)
    {
        await _dbContext.Qualifications.AddAsync(qualification);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Qualification qualification)
    {
        _dbContext.Qualifications.Update(qualification);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(GenericId id)
    {
        var qualification = await GetByIdAsync(id);
        qualification.Delete();
        _dbContext.Qualifications.Update(qualification);
        await _dbContext.SaveChangesAsync();
    }
}