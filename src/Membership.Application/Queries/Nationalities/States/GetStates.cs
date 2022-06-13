using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;

namespace Membership.Application.Queries.Nationalities.States;

public class GetStates : IQuery<IEnumerable<StateDto>>
{
}