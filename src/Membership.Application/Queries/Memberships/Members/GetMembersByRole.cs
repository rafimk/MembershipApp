﻿using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;

namespace Membership.Application.Queries.Memberships.Members;

public class GetMembersByRole : IQuery<IEnumerable<MemberDto>>
{
    public Guid UserId { get; set; }
}