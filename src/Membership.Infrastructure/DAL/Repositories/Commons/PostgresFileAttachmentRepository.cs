using Membership.Core.Entities.Commons;
using Membership.Core.Repositories.Commons;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Repositories.Commons;

internal sealed class PostgresFileAttachmentRepository : IFileAttachmentRepository
{
    private readonly MembershipDbContext _dbContext;

    public PostgresFileAttachmentRepository(MembershipDbContext dbContext)
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
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(FileAttachment fileAttachment)
    {
        _dbContext.FileAttachments.Update(fileAttachment);
        await _dbContext.SaveChangesAsync();
    }
}