﻿using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;

namespace Membership.Application.Queries.Memberships.Members;

public class GetMembershipCardPdf : IQuery<ReportDto>
{
    public Guid MemberId { get; set; }
}