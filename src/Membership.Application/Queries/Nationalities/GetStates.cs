using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;

namespace Membership.Application.Queries.Nationalities;

public class GetStates : IQuery<IEnumerable<StateDto>>
{
}