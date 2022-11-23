using Membership.Application.Abstractions;

namespace Membership.Application.Queries.Verifications;

public class DownloadEmiratesId : IQuery<bool>
{
    public int StartIndex { get; set; }
    public int NofRecord { get; set; }
}