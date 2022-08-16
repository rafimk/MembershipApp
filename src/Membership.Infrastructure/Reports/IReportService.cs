using Membership.Application.DTO.Memberships;

namespace Membership.Infrastructure.Reports;

public interface IReportService
{
    public Task<byte[]> GetMembershipCardAsync(MembershipCardDto membershipCardDto);
}