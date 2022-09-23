using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Memberships.Members;

public record UpdateVerification() : ICommand
{
    public Guid Id { get; set; }
    public bool EdiFrontAndBackSideValid { get; set; } 
    public bool EidNumberValid { get; set; } 
    public bool EidFullNameValid { get; set; } 
    public bool EidNationalityValid { get; set; }
    public bool EidDOBValid { get; set; }
    public bool EidDOEValid { get; set; }
    public bool PassportFirstPageValid { get; set; }
    public bool PassportLastPageValid { get; set; }
    public int CardType { get; set; }
    public int Gender { get; set; } 
    public bool EidIssuePlaceValid { get; set; }
    public Guid? VerifiedUserId { get; set; }
}