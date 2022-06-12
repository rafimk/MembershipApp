namespace Membership.Core.Abstractions;

public interface IClock
{
    DateTime Current();
}