using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Memberships;
using Membership.Core.Consts;
using Membership.Core.Contracts.Memberships;
using Membership.Core.Repositories.Memberships;

namespace Membership.Application.Commands.Memberships.Members.Handlers;

internal sealed class UpdateMemberHandler : ICommandHandler<UpdateMember>
{
    private readonly IMemberRepository _repository;

    public UpdateMemberHandler(IMemberRepository repository)
        => _repository = repository;

    public async Task HandleAsync(UpdateMember command)
    {
        var membership = await _repository.GetByIdAsync(command.Id);

        if (membership is null)
        {
            throw new MemberNotFoundException(command.Id);
        }
        
        var contract = new UpdateMemberContract
        {
            EmiratesIdFrontPage = command.EmiratesIdFrontPage,
            EmiratesIdLastPage = command.EmiratesIdLastPage,
            DateOfBirth = command.DateOfBirth,
            MobileNumber = command.MobileNumber,
            AlternativeContactNumber = command.AlternativeContactNumber,
            Email = command.Email,
            PassportNumber = command.PassportNumber,
            PassportExpiry = command.PassportExpiry,
            ProfessionId = command.ProfessionId,
            QualificationId = command.QualificationId,
            BloodGroup = (BloodGroup)command.BloodGroup,
            HouseName = command.HouseName,
            AddressInIndia = command.AddressInIndia,
            RegisteredOrganizationId = command.RegisteredOrganizationId,
            WelfareSchemeId = command.WelfareSchemeId,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = Guid.NewGuid()
        };
        membership.Update(contract);
        await _repository.UpdateAsync(membership);
    }
}