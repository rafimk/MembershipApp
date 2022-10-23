using Membership.Application.Abstractions;
using Membership.Core.ValueObjects;

namespace Membership.Application.Queries.Verifications;

public class AutoVerification : IQuery<bool>
{
    public DateTime ProcessDate { get; set; }
    public string FilePath { get; set; }
}