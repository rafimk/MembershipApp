using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Nationalities;
using Membership.Core.Repositories.Nationalities;

namespace Membership.Application.Commands.Nationalities.Districts.Handlers;

internal sealed class UpdateDistrictHandle : ICommandHandler<UpdateDistrict>
{
    private readonly IDistrictRepository _repository;

    public UpdateDistrictHandle(IDistrictRepository repository)
        => _repository = repository;

    public async Task HandleAsync(UpdateDistrict command)
    {
        var district = await _repository.GetByIdAsync(command.DistrictId);

        if (district is null)
        {
            throw new DistrictNotFoundException(command.DistrictId);
        }
        district.Update(command.Name);
        await _repository.UpdateAsync(district);
    }
}