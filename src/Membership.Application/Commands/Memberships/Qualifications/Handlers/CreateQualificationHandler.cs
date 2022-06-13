using Membership.Application.Abstractions;
using Membership.Core.Entities.Memberships.Qualifications;
using Membership.Core.Repositories.Memberships;

namespace Membership.Application.Commands.Memberships.Qualifications.Handlers;

internal sealed class CreateQualificationHandler : ICommandHandler<CreateQualification>
{
    private readonly IQualificationRepository _repository;

    public CreateQualificationHandler(IQualificationRepository repository)
        => _repository = repository;

    public async Task HandleAsync(CreateQualification command)
    {
        var qualification = Qualification.Create(Guid.NewGuid(), command.Name, DateTime.UtcNow);
        await _repository.AddAsync(qualification);
    }
}