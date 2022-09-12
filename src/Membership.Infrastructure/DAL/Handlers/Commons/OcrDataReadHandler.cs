using Membership.Application.Abstractions;
using Membership.Application.DTO.Commons;
using Membership.Application.Queries.Commons;
using Membership.Core.Abstractions;
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
    private readonly IMemberRepository _memberRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMembershipPeriodRepository _membershipPeriod;
    private readonly IClock _clock;
    
    public OcrDataReadHandler(IOcrService ocrService, IFileAttachmentRepository fileAttachmentRepository, IClock clock,
        IMemberRepository memberRepository, IUserRepository userRepository, IMembershipPeriodRepository membershipPeriod)
    {
        _ocrService = ocrService;
        _fileAttachmentRepository = fileAttachmentRepository;
        _clock = clock;
        _memberRepository = memberRepository;
        _userRepository = userRepository;
        _membershipPeriod = membershipPeriod;
    }
    public async Task<OcrDataDto> HandleAsync(OcrDataRead query)
    {
        var result = new OcrDataDto();

        var activePeriod = await _membershipPeriod.GetActivePeriodAsync();

        var membershipStartDate = _clock.Current();

        if (activePeriod is not null)
        {
            membershipStartDate = (DateTime)activePeriod?.RegistrationStarted;
        }
            

        var frontPage = await _fileAttachmentRepository.GetByIdAsync(query.FrontPageId.Value);

        if (frontPage is not null)
        {
            result = await GetOcrData(frontPage.FilePath, frontPage.SavedFileName, (Guid) query.UserId, result, membershipStartDate);
        }
 
        var lastPage = await _fileAttachmentRepository.GetByIdAsync(query.LastPageId.Value);

        if (lastPage is not null)
        {
            result = await GetOcrData(lastPage.FilePath, lastPage.SavedFileName , (Guid) query.UserId, result, membershipStartDate);
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

    private async Task<OcrDataDto> GetOcrData(string filePath, string readFileInfo, Guid userId, OcrDataDto result, DateTime membershipStartDate)
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

        if (result.DateofBirth is not null)
        {
            if (IsAgeLessThan18Years((DateTime) result.DateofBirth))
            {
                result.IsValidate = false;
                result.ErrorMessage = "Member should be 18 years old";
            }
        }
        
        if (result.ExpiryDate is not null)
        {
            if (result.ExpiryDate < membershipStartDate)
            {
                result.IsValidate = false;
                result.ErrorMessage = "Emirates ID is expired";
            }
        }
        
        if (ocrData.BackSideVerifyString is not null && result.IdNumber is not null)
        {
            var eid = result.IdNumber.Replace("-", "");
            if (!ocrData.BackSideVerifyString.Contains(eid))
            {
                result.IsValidate = false;
                result.ErrorMessage = "Emirates ID Backside Mismatch";
            }
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