using Membership.Application.Abstractions;
using Membership.Application.DTO.Commons;
using Membership.Application.Queries.Commons;
using Membership.Core.Entities.Commons;
using Membership.Core.Repositories.Commons;
using Membership.Infrastructure.OCR;

namespace Membership.Infrastructure.DAL.Handlers.Commons;

public class OcrDataReadHandler : IQueryHandler<OcrDataRead, OcrDataDto>
{
    private readonly IOcrService _ocrService;
    private readonly IFileAttachmentRepository _fileAttachmentRepository;
    private readonly IOcrResultRepository _ocrResultRepository;
    
    public OcrDataReadHandler(IOcrService ocrService, IFileAttachmentRepository fileAttachmentRepository, IOcrResultRepository ocrResultRepository)
    {
        _ocrService = ocrService;
        _fileAttachmentRepository = fileAttachmentRepository;
        _ocrResultRepository = ocrResultRepository;
    }
    public async Task<OcrDataDto> HandleAsync(OcrDataRead query)
    {
        var result = new OcrDataDto();
        
        var frontPage = await _fileAttachmentRepository.GetByIdAsync(query.FrontPageId.Value);

        if (frontPage is not null)
        {
            var uploadFilePath = GetFilePath(query.FilePath);

            if (!Directory.Exists(uploadFilePath))
            {
                Directory.CreateDirectory(uploadFilePath);
            }

            var readFileInfo = Path.Combine(uploadFilePath, frontPage.SavedFileName);

            var ocrData = await _ocrService.ReadData(readFileInfo, (Guid) query.UserId);

            if (ocrData.IdNumber is not null)
            {
                result.IdNumber = ocrData.IdNumber;
            }

            if (ocrData.Name is not null)
            {
                result.Name = ocrData.Name;
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
                result.CardNumber = ocrData.CardNumber;
            }
            
            if (result.CardType != ocrData.CardType)
            {
                result.CardType = ocrData.CardType;
            }
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

            var ocrData = await _ocrService.ReadData(readFileInfo, (Guid) query.UserId);

            if (ocrData.IdNumber is not null)
            {
                result.IdNumber = ocrData.IdNumber;
            }

            if (ocrData.Name is not null)
            {
                result.Name = ocrData.Name;
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
                result.CardNumber = ocrData.CardNumber;
            }
            
            if (result.CardType != ocrData.CardType)
            {
                result.CardType = ocrData.CardType;
            }
        }

        var ocrResultFrontPage = await _ocrResultRepository.GetByFrontPageIdAsync(query.FrontPageId);

        if (ocrResultFrontPage is not null)
        {
            ocrResultFrontPage.Update(query.FrontPageId, query.LastPageId, result.IdNumber, result.Name, result.DateofBirth,
                result.ExpiryDate, result.CardType, result.CardNumber);

            await _ocrResultRepository.UpdateAsync(ocrResultFrontPage);

            return result;
        }
        
        var ocrResultLastPage = await _ocrResultRepository.GetByFrontPageIdAsync(query.LastPageId);

        if (ocrResultLastPage is not null)
        {
            ocrResultLastPage.Update(query.FrontPageId, query.LastPageId, result.IdNumber, result.Name, result.DateofBirth,
                result.ExpiryDate, result.CardType, result.CardNumber);

            await _ocrResultRepository.UpdateAsync(ocrResultLastPage);
            
            return result;
        }

        var ocrResult1 = OcrResult.Create(Guid.NewGuid(),  query.FrontPageId, query.LastPageId, result.IdNumber, result.Name, result.DateofBirth, 
            result.ExpiryDate, result.CardNumber, result.CardType, DateTime.UtcNow, (Guid)query.UserId);

        await _ocrResultRepository.AddAsync(ocrResult1);
            
        return result;
    }
    
    private string GetFilePath(string filePath)
    {
        filePath ??= "UploadedFiles";
        return Path.GetFullPath(Path.Combine(Environment.CurrentDirectory + "\\", filePath));
    }
}