using Membership.Core.Entities.Commons;
using Membership.Core.ValueObjects;

namespace Membership.Core.Repositories.Commons;

public interface IFileAttachmentRepository
{
    Task<FileAttachment> GetByIdAsync(Guid id);
    Task<IEnumerable<FileAttachment>> GetAsync();
    Task AddAsync(FileAttachment fileAttachment);
    Task UpdateAsync(FileAttachment fileAttachment);
}