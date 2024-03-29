﻿using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Memberships.MembershipPeriods;

public record ActivateMembershipPeriod() : ICommand
{
    public Guid? MembershipPeriodId { get; set; }
}