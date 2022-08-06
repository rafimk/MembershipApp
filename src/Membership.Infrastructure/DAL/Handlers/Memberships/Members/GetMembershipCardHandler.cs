using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.Members;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.NETCore;

namespace Membership.Infrastructure.DAL.Handlers.Memberships.Members;

internal sealed class GetMembershipCardHandler : IQueryHandler<GetMembershipCard, ReportDto>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetMembershipCardHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<ReportDto> HandleAsync(GetMembershipCard query)
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

        string fileName = "ReceiptV1.rdlc";
        string filePath = "./Reports/";
  
        filePath = Path.Combine(filePath, fileName);

        using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            LocalReport report = new LocalReport();
            report.LoadReportDefinition(fileStream);
            report.SetParameters(new[]
            {
                new ReportParameter("MembershipNo", member?.MembershipId),
                new ReportParameter("FullName", member?.FullName),
                new ReportParameter("State", member?.Area?.State?.Name),
                new ReportParameter("District", member?.Mandalam?.District?.Name),
                new ReportParameter("Mandalam", member?.Mandalam?.Name),
                new ReportParameter("Panchayath", member?.Panchayat?.Name),
                new ReportParameter("MembershipDate", member?.CreatedAt.ToString("dd/MM/yyyy")),
                new ReportParameter("CollectedBy", agent?.FullName),
                new ReportParameter("Area", member?.Area?.Name)
            });

            var file = report.Render("PDF");

            return new ReportDto
            {
                File = file,
                FileType = "application/pdf",
                FileName = $"Membership-Card-{member?.MembershipId}.pdf"
            };
        }
    }
}