using Membership.Application.Abstractions;
using Membership.Application.Queries.Verifications;
using Membership.Core.Consts;
using Membership.Infrastructure.OCR;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Verifications;


internal sealed class AutoVerificationHandler : IQueryHandler<AutoVerification, bool>
{
    private readonly MembershipDbContext _dbContext;
    private readonly IOcrVerifyService _ocrVerifyService;
    
    public AutoVerificationHandler(MembershipDbContext dbContext, IOcrVerifyService ocrVerifyService)
    {
        _dbContext = dbContext;
        _ocrVerifyService = ocrVerifyService;
    }

    public async Task<bool> HandleAsync(AutoVerification query)
    {
        var result = await _dbContext.FileAttachments
            .Where(x => x.CreatedAt.Year == query.ProcessDate.Year &&
                        x.CreatedAt.Month == query.ProcessDate.Month &&
                        x.CreatedAt.Day == query.ProcessDate.Day &&
                        x.Type == FileType.PassportFirst)
            .OrderBy(x => x.CreatedAt)
            .ToListAsync();

        foreach (var item in result)
        {
            var output = await _ocrVerifyService.PassportService(item.FilePath, item.SavedFileName);
        }
       
        return true;
    }
}