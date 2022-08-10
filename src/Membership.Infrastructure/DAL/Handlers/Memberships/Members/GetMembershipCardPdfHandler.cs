using System.Drawing;
using System.Net.Http.Json;
using Membership.Application.Abstractions;
using Membership.Application.DTO.Commons;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.Members;
using Membership.Infrastructure.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SelectPdf;

namespace Membership.Infrastructure.DAL.Handlers.Memberships.Members;

internal sealed class GetMembershipCardPdfHandler : IQueryHandler<GetMembershipCardPdf, ReportDto>
{
    private readonly MembershipDbContext _dbContext;
    private readonly ReportsOptions _reportsOptions;
    private readonly ILogger<GetMembershipCardHandler> _logger;
    public GetMembershipCardPdfHandler(MembershipDbContext dbContext, 
        IOptions<ReportsOptions> reportsOptions, ILogger<GetMembershipCardHandler> logger)
    {
        _dbContext = dbContext;
        _reportsOptions = reportsOptions.Value;
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
        
        var membershipCardDto = new MembershipCardDto
        {
            MembershipNo = member?.MembershipId,
            Date = member?.CreatedAt.ToString("dd/MM/yyyy"),
            FullName = member?.FullName,
            District = member?.Mandalam?.District?.Name,
            Mandalam = member?.Mandalam?.Name,
            Panchayath = member?.Panchayat?.Name,
            State = member?.Area?.State?.Name,
            Area = member?.Area?.Name,
            CollectedBy = agent?.FullName
        };

        JsonContent content = JsonContent.Create(membershipCardDto);
        
        HttpClient _httpClient = new HttpClient();
        var response = await _httpClient.PostAsync($"{_reportsOptions.Url}reports/filestream", content);

        if (!response.IsSuccessStatusCode)
        {
            throw new ReporrServiceNotAvailable();
        } 

        return new ReportDto
        {
            File = response.Content.ReadAsByteArrayAsync().Result,
            FileType = "application/pdf",
            FileName = $"{member?.MembershipId}.pdf"
        };
        
    }
}