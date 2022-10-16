using ClosedXML.Excel;
using Membership.Application.DTO.Commons;
using Membership.Application.DTO.Memberships;
using Membership.Core.Abstractions;
using Membership.Core.Consts;
using Membership.Core.Entities.Commons;
using Membership.Core.Repositories.Commons;
using Microsoft.AspNetCore.Http;

namespace Membership.Infrastructure.FileManagement;

internal sealed class BufferedFileUploadLocalService : IBufferedFileUploadService
{
    private readonly IFileAttachmentRepository _attachmentRepository;
    private readonly IClock _clock;
    
    public BufferedFileUploadLocalService(IFileAttachmentRepository attachmentRepository, IClock clock)
    {
        _attachmentRepository = attachmentRepository;
        _clock = clock;
    }
    
    public async Task<Guid?> UploadFile(IFormFile file, string filePath, FileType type, Guid? userId)
    {
        if (file.Length > 0)
        {
            var uploadFilePath = GetFilePath(filePath);

            if (!Directory.Exists(uploadFilePath))
            {
                Directory.CreateDirectory(uploadFilePath);
            }

            var id = Guid.NewGuid();
            var newFileName = $"{id}___{file.FileName}";
            var extension = Path.GetExtension(file.FileName);
            var newPath = Path.Combine(uploadFilePath, newFileName);
            var fileType = extension.Replace(".", string.Empty);

            if (File.Exists(newPath))
            {
                throw new Exception("File Copy Failed");
            }

            using (var fileStream = new FileStream(newPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            var fileAttachment = FileAttachment.Create(id, type, file.FileName, newFileName,
                extension, $"application/{fileType}", newPath, file.Length, _clock.Current(), userId);

            await _attachmentRepository.AddAsync(fileAttachment);

            return id;
        }
        else
        {
            return null;
        }
    }

    public async Task<BufferedFileUploadDto> Download(Guid fileGuid, string filePath)
    {
        var file = await _attachmentRepository.GetByIdAsync(fileGuid);

        if (file is null)
        {
            throw new FileNotFoundException();
        }
        
        var uploadFilePath = GetFilePath(filePath);

        var memoryStream = new MemoryStream();

        if (!File.Exists(Path.Combine(uploadFilePath, file.SavedFileName)))
        {
            throw new FileNotFoundException();
        }
        
        using (var fileStream = new FileStream(Path.Combine(uploadFilePath, file.SavedFileName), FileMode.Open))
        {
            await fileStream.CopyToAsync(memoryStream);
        }
        
        memoryStream.Position = 0;

        return new BufferedFileUploadDto
        { 
            File = memoryStream.ToArray(),
            FileType = file.FileType,
            FileName = file.ActualFileName
        };
    }

    public BufferedFileUploadDto MembersExcelReportDownload(IEnumerable<MemberListDto> memberList)
    {
        using (var workBook = new XLWorkbook())
        {
            var workSheet = workBook.Worksheets.Add("Members");
            var currentRow = 1;

            #region Header
            workSheet.Cell(currentRow, 1).Value = "####";
            workSheet.Cell(currentRow, 2).Value = "Membership Id";
            workSheet.Cell(currentRow, 3).Value = "Name";
            workSheet.Cell(currentRow, 4).Value = "Emirates ID";
            workSheet.Cell(currentRow, 5).Value = "Mobile No";
            workSheet.Cell(currentRow, 6).Value = "State";
            workSheet.Cell(currentRow, 7).Value = "Area";
            workSheet.Cell(currentRow, 8).Value = "District";
            workSheet.Cell(currentRow, 9).Value = "Mandalam";
            workSheet.Cell(currentRow, 10).Value = "Panchayat";
            workSheet.Cell(currentRow, 11).Value = "Agent";
            #endregion

            #region Body

            var SlNo = 1;
            foreach (var member in memberList)
            {
                currentRow++;
                workSheet.Cell(currentRow, 1).Value = SlNo.ToString("#####");
                workSheet.Cell(currentRow, 2).Value = member.MembershipId;
                workSheet.Cell(currentRow, 3).Value = member.FullName;
                workSheet.Cell(currentRow, 4).Value = member.EmiratesIdNumber;
                workSheet.Cell(currentRow, 5).Value = member.MobileNumber;
                workSheet.Cell(currentRow, 6).Value = member.Area.State.Name;
                workSheet.Cell(currentRow, 7).Value = member.Area.Name;
                workSheet.Cell(currentRow, 8).Value = member.Mandalam.District.Name;
                workSheet.Cell(currentRow, 9).Value = member.Mandalam.Name;
                workSheet.Cell(currentRow, 10).Value = member.Panchayat.Name;
                workSheet.Cell(currentRow, 11).Value = member.Agent;
                SlNo++;
            }
            #endregion

            using (var stream = new MemoryStream())
            {
                workBook.SaveAs(stream);
                return new BufferedFileUploadDto
                { 
                    File =  stream.ToArray(),
                    FileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    FileName = "MembersList.xlsx"
                };
            }
        }
    }

    private string GetFilePath(string filePath)
    {
        filePath ??= "UploadedFiles";
        return Path.GetFullPath(Path.Combine(Environment.CurrentDirectory + "\\", filePath));
    }
}