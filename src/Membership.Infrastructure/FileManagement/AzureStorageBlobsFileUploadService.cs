using Azure.Storage.Blobs;
using ClosedXML.Excel;
using Membership.Application.DTO.Commons;
using Membership.Application.DTO.Memberships;
using Membership.Core.Abstractions;
using Membership.Core.Consts;
using Membership.Core.Entities.Commons;
using Membership.Core.Repositories.Commons;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Membership.Infrastructure.FileManagement;

public class AzureStorageBlobsFileUploadService : IBufferedFileUploadService
{
    private readonly IFileAttachmentRepository _attachmentRepository;
    private readonly IClock _clock;
    private readonly FileUploadOptions _fileUploadOptions;
    
    public AzureStorageBlobsFileUploadService(IFileAttachmentRepository attachmentRepository, 
        IClock clock, IOptions<FileUploadOptions> fileUploadOptions)
    {
        _attachmentRepository = attachmentRepository;
        _clock = clock;
        _fileUploadOptions = fileUploadOptions.Value;
    }
    public async Task<Guid?> UploadFile(IFormFile file, string filePath, FileType type, Guid? userId)
    {
        if (file.Length > 0)
        {
            var id = Guid.NewGuid();
            var newFileName = $"{id}___{file.FileName}";
            var extension = Path.GetExtension(file.FileName);
            var fileType = extension.Replace(".", string.Empty);
            var folderYear = _clock.Current().Year.ToString();

            string connectionString = _fileUploadOptions.StorageConnection;
            string containerName = $"{_fileUploadOptions.FilePath}-{type.ToString().ToLower()}-{folderYear}";
            
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            bool isExist =  containerClient.Exists();
            if (!isExist)
            {
                containerClient.Create();
            }

            BlobClient blobClient = containerClient.GetBlobClient(newFileName);

            if (file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    ms.Position = 0;
                    await blobClient.UploadAsync(ms, true);
                }
                
                var fileAttachment = FileAttachment.Create(id, type, file.FileName, newFileName,
                    extension, $"application/{fileType}", containerName, file.Length, _clock.Current(), userId);

                await _attachmentRepository.AddAsync(fileAttachment);

            }

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

        string connectionString = _fileUploadOptions.StorageConnection;
        string containerName = file.FilePath;
            
        BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

        BlobClient blobClient = containerClient.GetBlobClient(file.SavedFileName);
        
        using var stream = new MemoryStream();
        await blobClient.DownloadToAsync(stream);
        stream.Position = 0;

        return new BufferedFileUploadDto
        { 
            File =  stream.ToArray(),
            FileType = file.FileType,
            FileName = file.ActualFileName
        };
    }

    public BufferedFileUploadDto MembersExcelReportDownload(IEnumerable<MemberListDto> memberList)
    {
        using (var workBook = new XLWorkbook())
        {
            var workSheet = workBook.Worksheets.Add("Members");
            workSheet.Columns("A").Width = 10;
            workSheet.Columns("B").Width = 15;
            workSheet.Columns("C").Width = 50;
            workSheet.Columns("D").Width = 15;
            workSheet.Columns("E").Width = 25;
            workSheet.Columns("F").Width = 15;
            workSheet.Columns("G").Width = 15;
            workSheet.Columns("H").Width = 15;
            workSheet.Columns("I").Width = 15;
            workSheet.Columns("J").Width = 15;
            workSheet.Columns("K").Width = 10;
            workSheet.Columns("L").Width = 20;

            var currentRow = 2;

            #region Header
            workSheet.Cell(currentRow, 1).Value = "SI No.";
            workSheet.Cell(currentRow, 2).Value = "Membership No.";
            workSheet.Cell(currentRow, 3).Value = "Full Name";
            workSheet.Cell(currentRow, 4).Value = "Mobile";
            workSheet.Cell(currentRow, 5).Value = "Email";
            workSheet.Cell(currentRow, 6).Value = "State";
            workSheet.Cell(currentRow, 7).Value = "District";
            workSheet.Cell(currentRow, 8).Value = "Mandalam";
            workSheet.Cell(currentRow, 9).Value = "Panchayath";
            workSheet.Cell(currentRow, 10).Value = "Area";
            workSheet.Cell(currentRow, 11).Value = "Joined Date";
            workSheet.Cell(currentRow, 12).Value = "Agent Name";
           
            workSheet.Cell(currentRow,1).Style.Fill.SetBackgroundColor( XLColor.LightGreen );
            workSheet.Cell(currentRow,2).Style.Fill.SetBackgroundColor( XLColor.LightGreen );
            workSheet.Cell(currentRow,3).Style.Fill.SetBackgroundColor( XLColor.LightGreen );
            workSheet.Cell(currentRow,4).Style.Fill.SetBackgroundColor( XLColor.LightGreen );
            workSheet.Cell(currentRow,5).Style.Fill.SetBackgroundColor( XLColor.LightGreen );
            workSheet.Cell(currentRow,6).Style.Fill.SetBackgroundColor( XLColor.LightGreen );
            workSheet.Cell(currentRow,7).Style.Fill.SetBackgroundColor( XLColor.LightGreen );
            workSheet.Cell(currentRow,8).Style.Fill.SetBackgroundColor( XLColor.LightGreen );
            workSheet.Cell(currentRow,9).Style.Fill.SetBackgroundColor( XLColor.LightGreen );
            workSheet.Cell(currentRow,10).Style.Fill.SetBackgroundColor( XLColor.LightGreen );
            workSheet.Cell(currentRow,11).Style.Fill.SetBackgroundColor( XLColor.LightGreen );
            workSheet.Cell(currentRow,12).Style.Fill.SetBackgroundColor( XLColor.LightGreen );
            #endregion

            #region Body

            var SlNo = 1;
            foreach (var member in memberList)
            {
                currentRow++;
                workSheet.Cell(currentRow, 1).Value = SlNo.ToString("####");
                workSheet.Cell(currentRow, 2).Value = member.MembershipId;
                workSheet.Cell(currentRow, 3).Value = member.FullName;
                workSheet.Cell(currentRow, 4).Value = member.MobileNumber;
                workSheet.Cell(currentRow, 5).Value = member.Email;
                workSheet.Cell(currentRow, 6).Value = member.State.Name;
                workSheet.Cell(currentRow, 7).Value = member.District.Name;
                workSheet.Cell(currentRow, 8).Value = member.Mandalam.Name;
                workSheet.Cell(currentRow, 9).Value = member.Panchayat.Name;
                workSheet.Cell(currentRow, 10).Value = member.Area.Name;
                workSheet.Cell(currentRow, 11).Value = member.CreatedAt.ToString("dd/MM/yyyy");
                workSheet.Cell(currentRow, 12).Value = member.Agent;
                SlNo++;
            }
            #endregion
            
            IXLRange range = workSheet.Range(workSheet.Cell(2,1).Address, workSheet.Cell(currentRow,12).Address);

            range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
            range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            range.Style.Border.RightBorder = XLBorderStyleValues.Thin;
            range.Style.Border.BottomBorder = XLBorderStyleValues.Thin;

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
}