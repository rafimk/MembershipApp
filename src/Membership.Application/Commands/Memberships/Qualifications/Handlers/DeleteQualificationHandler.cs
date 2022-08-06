using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Memberships;
using Membership.Core.Repositories.Memberships;

namespace Membership.Application.Commands.Memberships.Qualifications.Handlers;

internal sealed class DeleteQualificationHandler : ICommandHandler<DeleteQualification>
{
    private readonly IQualificationRepository _repository;

    public DeleteQualificationHandler(IQualificationRepository repository)
        => _repository = repository;

    public async Task HandleAsync(DeleteQualification command)
    {
        var qualification = await _repository.GetByIdAsync((Guid)command.QualificationId);

        if (qualification is null)
        {
            throw new QualificationNotFoundException(command.QualificationId);
        }
        qualification.Delete();
        await _repository.UpdateAsync(qualification);
    }
}