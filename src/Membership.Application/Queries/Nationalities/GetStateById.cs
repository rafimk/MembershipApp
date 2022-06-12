using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;

namespace Membership.Application.Queries.Nationalities;

public class GetStateById : IQuery<StateDto>
{
    public Guid StateId { get; set; }
}