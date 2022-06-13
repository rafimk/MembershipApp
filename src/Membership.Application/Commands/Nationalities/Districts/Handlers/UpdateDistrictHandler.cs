using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Nationalities.Districts.Handlers;

internal sealed class UpdateDistrictHandle : ICommandHandler<UpdateDistrict>
{
    private readonly IDistrictRepository _repository;

    public UpdateDistrictHandle(IDistrictRepository repository)
        => _repository = repository;

    public async Task HandleAsync(UpdateDistrict command)
    {
        var district = await _repository.GetByIdAsync(command.Id);

        if (district is null)
        {
            throw new DistrictNotFoundException(command.Id);
        }
        district.Update(command.Name);
        await _repository.UpdateAsync(district);
    }
}