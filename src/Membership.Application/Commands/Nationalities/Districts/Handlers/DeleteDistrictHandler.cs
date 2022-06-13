using Membership.Application.Abstractions;
using Membership.Core.Entities.Nationalities;
using Membership.Core.Repositories.Nationalities;

namespace Membership.Application.Commands.Nationalities.Districts.Handlers;

internal sealed class DeleteDistrictHandler : ICommandHandler<DeleteDistrict>
{
    private readonly IDistrictRepository _repository;

    public DeleteDistrictHandler(IDistrictRepository repository)
        => _repository = repository;

    public async Task HandleAsync(DeleteDistrict command)
    {
        var district = await _repository.GetByIdAsync(command.Id);

        if (district is null)
        {
            throw new DistrictNotFoundException(command.Id);
        }
        district.Delete();
        await _repository.UpdateAsync(district);
    }
}