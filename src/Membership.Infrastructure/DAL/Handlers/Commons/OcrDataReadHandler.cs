using Membership.Application.Abstractions;
using Membership.Application.DTO.Commons;
using Membership.Application.Queries.Commons;
using Membership.Core.Entities.Commons;
using Membership.Core.Repositories.Commons;
using Membership.Core.Repositories.Memberships;
using Membership.Infrastructure.OCR;

namespace Membership.Infrastructure.DAL.Handlers.Commons;

public class OcrDataReadHandler : IQueryHandler<OcrDataRead, OcrDataDto>
{
    private readonly IOcrService _ocrService;
    private readonly IFileAttachmentRepository _fileAttachmentRepository;
    private readonly IOcrResultRepository _ocrResultRepository;
    private readonly IMemberRepository _memberRepository;
    
    public OcrDataReadHandler(IOcrService ocrService, IFileAttachmentRepository fileAttachmentRepository, 
        IOcrResultRepository ocrResultRepository, IMemberRepository memberRepository)
    {
        _ocrService = ocrService;
        _fileAttachmentRepository = fileAttachmentRepository;
        _ocrResultRepository = ocrResultRepository;
        _memberRepository = memberRepository;
    }
    public async Task<OcrDataDto> HandleAsync(OcrDataRead query)
    {
        var result = new OcrDataDto();

        var availableOcrResult = await _ocrResultRepository.GetByFrontPageIdAsync(query.FrontPageId.Value);

        if (availableOcrResult is null)
        {
            var frontPage = await _fileAttachmentRepository.GetByIdAsync(query.FrontPageId.Value);

            if (frontPage is not null)
            {
                var uploadFilePath = GetFilePath(query.FilePath);

                if (!Directory.Exists(uploadFilePath))
                {
                    Directory.CreateDirectory(uploadFilePath);
                }

                var readFileInfo = Path.Combine(uploadFilePath, frontPage.SavedFileName);

                result = await GetOcrData(readFileInfo, (Guid) query.UserId, result);
            }
        }
        else
        {
            result = GetFromRsult(availableOcrResult, result);

            if (availableOcrResult.LastPageId.Value == query.LastPageId.Value)
            {
                return result;
            }
        }
        
        availableOcrResult = await _ocrResultRepository.GetByFrontPageIdAsync(query.LastPageId.Value);

        if (availableOcrResult is not null)
        {
            result = GetFromRsult(availableOcrResult, result);
            return result;
        }

        var lastPage = await _fileAttachmentRepository.GetByIdAsync(query.LastPageId.Value);

        if (lastPage is not null)
        {
            var uploadFilePath = GetFilePath(query.FilePath);

            if (!Directory.Exists(uploadFilePath))
            {
                Directory.CreateDirectory(uploadFilePath);
            }

            var readFileInfo = Path.Combine(uploadFilePath, lastPage.SavedFileName);

            result = await GetOcrData(readFileInfo, (Guid) query.UserId, result);
        }

        var ocrResult = OcrResult.Create(Guid.NewGuid(),  query.FrontPageId, query.LastPageId, result.IdNumber, result.Name, result.DateofBirth, 
            result.ExpiryDate, result.CardNumber, result.CardType, DateTime.UtcNow, (Guid)query.UserId);

        await _ocrResultRepository.AddAsync(ocrResult);

        var member = await _memberRepository.GetByEmiratesIdAsync(result.IdNumber);

        if (member is not null)
        {
            result.IsDispute = true;
        }
            
        return result;
    }

    private async Task<OcrDataDto> GetOcrData(string readFileInfo, Guid userId, OcrDataDto result)
    {
        var ocrData = await _ocrService.ReadData(readFileInfo, userId);
        
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
            result.DateofBirth = availableOcrResult.DateofBirth;
        }

        if (availableOcrResult.ExpiryDate is not null)
        {
            result.ExpiryDate = availableOcrResult.ExpiryDate;
        }

        if (availableOcrResult.CardNumber is not null)
        {
            result.CardNumber = availableOcrResult.CardNumber;
        }
            
        if (availableOcrResult.CardType != result.CardType)
        {
            result.CardType = availableOcrResult.CardType;
        }

        return result;
    }
    
    private string GetFilePath(string filePath)
    {
        filePath ??= "UploadedFiles";
        return Path.GetFullPath(Path.Combine(Environment.CurrentDirectory + "\\", filePath));
    }
}