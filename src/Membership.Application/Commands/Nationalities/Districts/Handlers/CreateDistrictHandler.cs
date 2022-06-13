using Membership.Application.Abstractions;
using Membership.Core.Entities.Nationalities;
using Membership.Core.Repositories.Nationalities;

namespace Membership.Application.Commands.Nationalities.Districts.Handlers;


internal sealed class CreateDistrictHandler : ICommandHandler<CreateDistrict>
{
    private readonly IDistrictRepository _repository;

    public CreateDistrictHandler(IDistrictRepository repository)
        => _repository = repository;

    public async Task HandleAsync(CreateDistrict command)
    {
        var district = District.Create(Guid.NewGuid(), command.Name, DateTime.UtcNow);
        await _repository.AddAsync(district);
    }
}