using Membership.Core.Exceptions;

namespace Membership.Infrastructure.Exceptions;

public class FileAlreadyExistException : CustomException
{
    public string FileName { get; }
    
    public FileAlreadyExistException(string fileName) : base($"File name : {fileName} already exist.")
    {
        FileName = fileName;
    }
}