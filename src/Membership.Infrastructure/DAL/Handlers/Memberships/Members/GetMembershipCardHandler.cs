using DinkToPdf;
using DinkToPdf.Contracts;
using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.Members;
using Membership.Infrastructure.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Reporting.NETCore;

namespace Membership.Infrastructure.DAL.Handlers.Memberships.Members;

internal sealed class GetMembershipCardHandler : IQueryHandler<GetMembershipCard, MemberCardDto>
{
    private readonly MembershipDbContext _dbContext;
    private IConverter _converter;
    private readonly ILogger<GetMembershipCardHandler> _logger;

    public GetMembershipCardHandler(MembershipDbContext dbContext, ILogger<GetMembershipCardHandler> logger,
        IConverter converter)
    {
        _dbContext = dbContext;
        _logger = logger;
        _converter = converter;
    }

    public async Task<MemberCardDto> HandleAsync(GetMembershipCard query)
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


        return new MemberCardDto
        {
            MembershipId = member?.MembershipId,
            Date = member?.CreatedAt.ToString("dd/MM/yyyy"),
            FullName = member?.FullName,
            District = member?.Mandalam?.District?.Name,
            Mandalam = member?.Mandalam?.Name,
            Panchayath = member?.Panchayat?.Name,
            Emirate = member?.Area?.State?.Name,
            Area = member?.Area?.Name,
            CollectedBy = agent?.FullName
        };
        
        // var globalSettings = new GlobalSettings
        // {
        //     ColorMode = ColorMode.Color,
        //     Orientation = Orientation.Portrait,
        //     PaperSize = PaperKind.A3,
        //     Margins = new MarginSettings { Top = 10 },
        //     DocumentTitle = "Membership Card",
        //     Out = reportFileName
        // };
        // var objectSettings = new ObjectSettings
        // {
        //     PagesCount = true,
        //     HtmlContent = TemplateGenerator.GetHTMLString(member?.MembershipId,
        //         member?.CreatedAt.ToString("dd/MM/yyyy"),
        //         member?.FullName,
        //         member?.Mandalam?.District?.Name,
        //         member?.Mandalam?.Name,
        //         member?.Panchayat?.Name,
        //         member?.Area?.State?.Name,
        //         member?.Area?.Name,
        //         agent?.FullName
        //         ),
        //     WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet =  stylesheetfilePath },
        // };
        // var pdf = new HtmlToPdfDocument()
        // {
        //     GlobalSettings = globalSettings,
        //     Objects = { objectSettings }
        // };
        //
        // _converter.Convert(pdf);
        // var memoryStream = new MemoryStream();
        //
        // using (var fileStream = new FileStream( Path.Combine(filePath, fileName), FileMode.Open))
        // {
        //     await fileStream.CopyToAsync(memoryStream);
        // }
        //
        // memoryStream.Position = 0;
        //
        // // using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        // // {
        // //     LocalReport report = new LocalReport();
        // //     report.LoadReportDefinition(fileStream);
        // //     report.SetParameters(new[]
        // //     {
        // //         new ReportParameter("MembershipNo", member?.MembershipId),
        // //         new ReportParameter("FullName", member?.FullName),
        // //         new ReportParameter("State", member?.Area?.State?.Name),
        // //         new ReportParameter("District", member?.Mandalam?.District?.Name),
        // //         new ReportParameter("Mandalam", member?.Mandalam?.Name),
        // //         new ReportParameter("Panchayath", member?.Panchayat?.Name),
        // //         new ReportParameter("MembershipDate", member?.CreatedAt.ToString("dd/MM/yyyy")),
        // //         new ReportParameter("CollectedBy", agent?.FullName),
        // //         new ReportParameter("Area", member?.Area?.Name)
        // //     });
        // //
        // //    var file = report.Render("PDF");
        // //}
        //
        // return new ReportDto
        // {
        //     File = memoryStream.ToArray(),
        //     FileType = "application/pdf",
        //     FileName = $"{member?.MembershipId}.pdf"
        // };
        
    }
}