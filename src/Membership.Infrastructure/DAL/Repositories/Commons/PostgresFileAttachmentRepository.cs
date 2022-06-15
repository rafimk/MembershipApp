using Membership.Core.Entities.Nationalities;
using Membership.Core.Repositories.Nationalities;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Repositories.Memberships;

internal sealed class PostgresFileAttachmentRepository : IProfessionRepository
{
    private readonly MMSDbContext _dbContext;

    public PostgresFileAttachmentRepository(MMSDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public  Task<FileAttachment> GetByIdAsync(Guid id)
        => _dbContext.FileAttachments.SingleOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<FileAttachment>> GetAsync()
        => await _dbContext.FileAttachments.Where(x => !x.IsDeleted).ToListAsync();

    public async Task AddAsync(FileAttachment fileAttachment)
    {
        await _dbContext.FileAttachments.AddAsync(fileAttachment);
    }

    public async Task UpdateAsync(FileAttachment fileAttachment)
    {
        _dbContext.FileAttachments.Update(fileAttachment);
    }
}