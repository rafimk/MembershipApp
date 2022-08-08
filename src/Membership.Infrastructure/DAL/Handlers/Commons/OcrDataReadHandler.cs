using Membership.Application.Abstractions;
using Membership.Application.DTO.Commons;
using Membership.Application.Queries.Commons;
using Membership.Core.Consts;
using Membership.Core.Entities.Commons;
using Membership.Core.Repositories.Commons;
using Membership.Core.Repositories.Memberships;
using Membership.Core.Repositories.Users;
using Membership.Infrastructure.OCR;

namespace Membership.Infrastructure.DAL.Handlers.Commons;

public class OcrDataReadHandler : IQueryHandler<OcrDataRead, OcrDataDto>
{
    private readonly IOcrService _ocrService;
    private readonly IFileAttachmentRepository _fileAttachmentRepository;
    private readonly IOcrResultRepository _ocrResultRepository;
    private readonly IMemberRepository _memberRepository;
    private readonly IUserRepository _userRepository;
    
    public OcrDataReadHandler(IOcrService ocrService, IFileAttachmentRepository fileAttachmentRepository, 
        IOcrResultRepository ocrResultRepository, IMemberRepository memberRepository, IUserRepository userRepository)
    {
        _ocrService = ocrService;
        _fileAttachmentRepository = fileAttachmentRepository;
        _ocrResultRepository = ocrResultRepository;
        _memberRepository = memberRepository;
        _userRepository = userRepository;
    }
    public async Task<OcrDataDto> HandleAsync(OcrDataRead query)
    {
        var result = new OcrDataDto();

        var frontPage = await _fileAttachmentRepository.GetByIdAsync(query.FrontPageId.Value);

        if (frontPage is not null)
        {
            result = await GetOcrData(frontPage.FilePath, frontPage.SavedFileName, (Guid) query.UserId, result);
        }
 
        var lastPage = await _fileAttachmentRepository.GetByIdAsync(query.LastPageId.Value);

        if (lastPage is not null)
        {
            result = await GetOcrData(lastPage.FilePath, lastPage.SavedFileName , (Guid) query.UserId, result);
        }

        var ocrResult = OcrResult.Create(Guid.NewGuid(),  query.FrontPageId, query.LastPageId, result.IdNumber, result.Name, result.DateofBirth, 
            result.ExpiryDate, result.CardNumber, result.CardType, DateTime.UtcNow, (Guid)query.UserId);

        // await _ocrResultRepository.AddAsync(ocrResult);

        if (result.IdNumber is not null)
        {
            var member = await _memberRepository.GetByEmiratesIdAsync(result.IdNumber);

            if (member is not null)
            {
                var agentInfo = await _userRepository.GetByIdAsync((Guid) query.UserId);

                if (agentInfo is not null && agentInfo.MandalamId == member.MandalamId)
                {
                    result.IsDuplicate = true;
                }

                result.IsDispute = true;
            }
        }

        if (result.DateofBirth.HasValue)
        {
            if (IsAgeLessThan18Years(result.DateofBirth.Value))
            {
                result.IsValidate = false;
                result.ErrorMessage = "Age Under 18 not allowed";
            }
        }

        return result;
    }

    private async Task<OcrDataDto> GetOcrData(string filePath, string readFileInfo, Guid userId, OcrDataDto result)
    {
        var ocrData = await _ocrService.ReadData(filePath, readFileInfo, userId);
        
        if (ocrData.IdNumber is not null)
        {
            result.IdNumber = ocrData.IdNumber.Trim();
        }

        if (ocrData.Name is not null)
        {
            result.Name = ocrData.Name.Trim();
        }

        if (ocrData.DateofBirth is not null)
        {
            result.DateofBirth = ocrData.DateofBirth;
        }

        if (ocrData.ExpiryDate is not null)
        {
            result.ExpiryDate = ocrData.ExpiryDate;
        }

        if (ocrData.CardNumber is not null)
        {
            result.CardNumber = ocrData.CardNumber.Trim();
        }
            
        if (result.CardType != ocrData.CardType)
        {
            result.CardType = ocrData.CardType;
        }

        if ((result.IdNumber is null)&& result.Name is null &&
            result.DateofBirth is null && result.ExpiryDate is null)
        {
            result.Status = OcrStatus.NoDataAvailable;
            result.IsValidate = false;
        }
        else if (result.IdNumber is not null && result.Name is not null &&
                     result.DateofBirth is not null && result.ExpiryDate is not null)
        {
            if (result.IdNumber.Trim().Length != 0 && result.Name.Trim().Length != 0)
            {
                result.Status = OcrStatus.Verified;
                result.IsValidate = true;
            }
            else
            {
                result.Status = OcrStatus.PartiallyVerified;
                result.IsValidate = true;
            }
          
        }
        else
        {
            result.Status = OcrStatus.PartiallyVerified;
            result.IsValidate = true;
        }

        return result;
    }

    private OcrDataDto GetFromRsult(OcrResult availableOcrResult, OcrDataDto result)
    {
        if (availableOcrResult.IdNumber is not null)
        {
            result.IdNumber = availableOcrResult.IdNumber;
        }

        if (availableOcrResult.Name is not null)
        {
            result.Name = availableOcrResult.Name;
        }

        if (availableOcrResult.DateofBirth is not null)
        {
            result.DateofBirth = availableOcrResult.DateofBirth.Value.Date;
        }

        if (availableOcrResult.ExpiryDate is not null)
        {
            result.ExpiryDate = availableOcrResult.ExpiryDate.Value.Date;
        }

        if (availableOcrResult.CardNumber is not null)
        {
            result.CardNumber = availableOcrResult.CardNumber;
        }
            
        if (availableOcrResult.CardType != result.CardType)
        {
            result.CardType = availableOcrResult.CardType;
        }
        
        if (IsAgeLessThan18Years(result.DateofBirth.Value))
        {
            result.IsValidate = false;
            result.ErrorMessage = "Age Under 18 not allowed";
        }

        return result;
    }
    
    private string GetFilePath(string filePath)
    {
        filePath ??= "UploadedFiles";
        return Path.GetFullPath(Path.Combine(Environment.CurrentDirectory + "\\", filePath));
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