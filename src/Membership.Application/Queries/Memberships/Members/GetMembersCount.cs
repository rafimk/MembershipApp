using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;

namespace Membership.Application.Queries.Memberships.Members;

public class GetMembersCount : IQuery<IEnumerable<MemberDto>>
{
    public Guid CascadeId { get; set; }
    public UserRole Role { get; set;} 
}