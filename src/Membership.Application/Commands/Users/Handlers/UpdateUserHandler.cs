using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Users;
using Membership.Core.Repositories.Users;

namespace Membership.Application.Commands.Nationalities.Areas.Handlers;

internal sealed class UpdateUserHandler : ICommandHandler<UpdateUser>
{
    private readonly IUserRepository _repository;

    public UpdateUserHandler(IUserRepository repository)
        => _repository = repository;

    public async Task HandleAsync(UpdateUser command)
    {
        var user = await _repository.GetByIdAsync(command.UserId);

        if (user is null)
        {
            throw new UserNotFoundException(command.UserId);
        }

        var contract = new UpdateCreateContract
        {
            FullName = command.FullName;
            MobileNumber = command.MobileNumber;
            AlternativeContactNumber = command.AlternativeContactNumber;
            Designation = command.Designation;
        };

        user.Update(contract);
        await _repository.UpdateAsync(user);
    }
}