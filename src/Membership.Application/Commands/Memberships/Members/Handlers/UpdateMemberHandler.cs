using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Memberships;
using Membership.Core.Consts;
using Membership.Core.Contracts.Memberships;
using Membership.Core.Exceptions.Memberships;
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
        
        if (!IsAgeLessThan18Years(command.DateOfBirth))
        {
            throw new AgeLessThan18YearsException();
        }
        
        if (command.EmiratesIdExpiry <= DateTime.Today )
        {
            throw new InvalidEmiratesIdExpiryDate();
        }
        
        var contract = new UpdateMemberContract
        {
            EmiratesIdFrontPage = command.EmiratesIdFrontPage,
            EmiratesIdLastPage = command.EmiratesIdLastPage,
            EmiratesIdExpiry = command.EmiratesIdExpiry,
            DateOfBirth = command.DateOfBirth,
            MobileNumber = command.MobileNumber,
            AlternativeContactNumber = command.AlternativeContactNumber,
            Email = command.Email,
            PassportNumber = command.PassportNumber,
            PassportExpiry = command.PassportExpiry,
            PassportFrontPage = command.PassportFrontPage,
            PassportLastPage = command.PassportLastPage,
            ProfessionId = command.ProfessionId,
            QualificationId = command.QualificationId,
            Gender = (Gender)command.Gender,
            Photo = command.Photo,
            BloodGroup = (BloodGroup)command.BloodGroup,
            HouseName = command.HouseName,
            AddressInIndia = command.AddressInIndia,
            AddressInDistrictId = command.AddressInDistrictId,
            AddressInMandalamId = command.AddressInMandalamId,
            AddressInPanchayatId = command.AddressInPanchayatId,
            RegisteredOrganizationId = command.RegisteredOrganizationId,
            WelfareSchemeId = command.WelfareSchemeId,
        };
        membership.Update(contract);
        await _repository.UpdateAsync(membership);
    }
    
    private bool IsAgeLessThan18Years(DateTime birthDate)
    {
        if (DateTime.Now.Year - birthDate.Year > 18)
        {
            return false;
        }
        else if (DateTime.Now.Year - birthDate.Year < 18)
        {
            return true;
        }
        else //if (DateTime.Now.Year - birthDate.Year == 18)
        {
            if (birthDate.DayOfYear < DateTime.Now.DayOfYear)
            {
                return false;
            }
            else if (birthDate.DayOfYear > DateTime.Now.DayOfYear)
            {
                return true;
            }
            else //if (birthDate.DayOfYear == DateTime.Now.DayOfYear)
            {
                return false;
            }
        }
    }
}