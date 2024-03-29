﻿using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;

namespace Membership.Application.Queries.Memberships.Members;

public class GetMembershipCard : IQuery<MemberCardDto>
{
    public Guid MemberId { get; set; }
}