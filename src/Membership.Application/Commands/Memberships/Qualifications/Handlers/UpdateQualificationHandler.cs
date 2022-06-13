using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Memberships;
using Membership.Core.Repositories.Memberships;

namespace Membership.Application.Commands.Memberships.Qualifications.Handlers;

internal sealed class UpdateQualificationHandler : ICommandHandler<UpdateQualification>
{
    private readonly IQualificationRepository _repository;

    public UpdateQualificationHandler(IQualificationRepository repository)
        => _repository = repository;

    public async Task HandleAsync(UpdateQualification command)
    {
        var qualification = await _repository.GetByIdAsync(command.QualificationId);

        if (qualification is null)
        {
            throw new QualificationNotFoundException(command.QualificationId);
        }
        qualification.Update(command.Name);
        await _repository.UpdateAsync(qualification);
    }
}