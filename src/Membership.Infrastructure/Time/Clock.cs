using Membership.Core.Abstractions;

namespace Membership.Infrastructure.Time;

internal sealed class Clock : IClock
{
    public DateTime Current() => DateTime.UtcNow;
}