using Membership.Core.Entities.Commons;

namespace Membership.Core.Repositories.Commons;

public interface IFileAttachmentRepository
{
    Task<FileAttachment> GetByIdAsync(Guid id);
    Task AddAsync(FileAttachment fileAttachment);
    Task DeleteAsync(Guid id);
}