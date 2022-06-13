using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Nationalities.Mandalams.Hanlers;

internal sealed class CreateMandalamHandler : ICommandHandler<CreateMandalam>
{
    private readonly IMandalamRepository _repository;

    public CreateMandalamHandler(IMandalamRepository repository)
        => _repository = repository;

    public async Task HandleAsync(CreateMandalam command)
    {
        var mandalam = new Mandalam();
        mandalam.Create(Guid.NewGuid(), command.Name, command.DistrictId, DateTime.UtcNow);
        await _repository.AddAsync(mandalam);
    }
}