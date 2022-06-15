using Membership.Core.Exceptions;

namespace Membership.Application.Exceptions.Memberships;

public class FileAlreadyExistException : CustomException
{
    
    public FileAlreadyExistException() : base($"File name already exist.");
}