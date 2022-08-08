using System.Drawing;
using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.Members;
using Membership.Infrastructure.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SelectPdf;

namespace Membership.Infrastructure.DAL.Handlers.Memberships.Members;

internal sealed class GetMembershipCardPdfHandler : IQueryHandler<GetMembershipCardPdf, ReportDto>
{
    private readonly MembershipDbContext _dbContext;
    private readonly ILogger<GetMembershipCardHandler> _logger;

    public GetMembershipCardPdfHandler(MembershipDbContext dbContext, ILogger<GetMembershipCardHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<ReportDto> HandleAsync(GetMembershipCardPdf query)
    {
        var memberId = query.MemberId;
        var member = await _dbContext.Members
            .Include(x => x.Profession)
            .Include(x => x.Qualification)
            .Include(x => x.Mandalam).ThenInclude(x => x.District)
            .Include(x => x.Panchayat)
            .Include(x => x.Area).ThenInclude(x => x.State)
            .Include(x => x.MembershipPeriod)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == memberId);

        var result = member?.AsDto();

        if (member?.AddressInDistrictId is not null)
        {
            var addressInDistrict =
                await _dbContext.Districts.SingleOrDefaultAsync(x =>
                    x.Id == (Guid) member.AddressInDistrictId);
            if (addressInDistrict is not null)
            {
                result.AddressInDistrict = addressInDistrict.AsDto();
            }
        }

        if (member?.AddressInMandalamId is not null)
        {
            var addressInMandalam =
                await _dbContext.Mandalams.SingleOrDefaultAsync(x =>
                    x.Id == (Guid) member.AddressInMandalamId);
            if (addressInMandalam is not null)
            {
                result.AddressInMandalam = addressInMandalam.AsDto();
            }
        }
        
        if (member?.AddressInPanchayatId is not null)
        {
            var addressInPanchayat =
                await _dbContext.Panchayats.SingleOrDefaultAsync(x =>
                    x.Id == (Guid) member.AddressInPanchayatId);
            if (addressInPanchayat is not null)
            {
                result.AddressInPanchayat = addressInPanchayat.AsDto();
            }
        }

        var agent = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == member.CreatedBy);

        string fileName = $"{member?.MembershipId.Value.ToString()}.pdf";
        string filePath = "./Reports/";
        
        string stylesheetfileName = $"styles.css";
  
        var reportFileName = Path.Combine(filePath, fileName);
        var stylesheetfilePath =  Path.Combine(filePath, stylesheetfileName);
        
        _logger.LogInformation($"RDLC File : {reportFileName}");


        var html = TemplateGenerator.GetHTMLString(member?.MembershipId,
            member?.CreatedAt.ToString("dd/MM/yyyy"),
            member?.FullName,
            member?.Mandalam?.District?.Name,
            member?.Mandalam?.Name,
            member?.Panchayat?.Name,
            member?.Area?.State?.Name,
            member?.Area?.Name,
            agent?.FullName
        );
        
        var converter = new HtmlToPdf();

        converter.Options.PdfPageSize = PdfPageSize.Custom;
        converter.Options.PdfPageCustomSize = new SizeF(200, 300);

        var pdfDocument = converter.ConvertHtmlString(html);
        byte[] pdf = pdfDocument.Save();

        pdfDocument.Close();

        return new ReportDto
        {
            File = pdf,
            FileType = "application/pdf",
            FileName = $"{member?.MembershipId}.pdf"
        };
        
    }
}