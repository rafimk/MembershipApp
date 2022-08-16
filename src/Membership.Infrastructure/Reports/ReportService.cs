using System.Net.Http.Json;
using Membership.Application.DTO.Commons;
using Membership.Application.DTO.Memberships;
using Microsoft.Extensions.Options;

namespace Membership.Infrastructure.Reports;

public class ReportService : IReportService
{
    private readonly HttpClient _httpClient;
    private readonly ReportsOptions _reportsOptions;
    
    public ReportService(HttpClient httpClient, IOptions<ReportsOptions> reportsOptions)
    {
        _httpClient = httpClient;
        _reportsOptions = reportsOptions.Value;
    }
    public async Task<byte[]> GetMembershipCardAsync(MembershipCardDto membershipCardDto)
    {
        JsonContent content = JsonContent.Create(membershipCardDto);
        
        var response = await _httpClient.PostAsync($"{_reportsOptions.Url}reports/filestream", content);

        if (!response.IsSuccessStatusCode)
        {
            throw new ReporrServiceNotAvailable();
        }

        return response.Content.ReadAsByteArrayAsync().Result;
    }
}