using System.Text;
using Azure.Storage.Blobs;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Extensions.Options;

namespace Membership.Infrastructure.OCR;

public class OcrVerifyService : IOcrVerifyService
{
    private readonly string _subscriptionKey = "d4f537561bdd405489046ac0e633cdc0";
    private readonly string _endpoint = "https://uaekmcc.cognitiveservices.azure.com/";
    private readonly ComputerVisionClient _client;
    private readonly FileUploadOptions _fileUploadOptions;
    public OcrVerifyService(IOptions<FileUploadOptions> fileUploadOptions)
    {
        _fileUploadOptions = fileUploadOptions.Value;
    }
    
    public async Task<string> PassportService(string filePath, string fileInfo)
    {
        string connectionString = _fileUploadOptions.StorageConnection;
        string containerName = filePath;
            
        BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

        BlobClient blobClient = containerClient.GetBlobClient(fileInfo);
        
        
        using var memoryStream = new MemoryStream();
        await blobClient.DownloadToAsync(memoryStream);
        memoryStream.Position = 0;
        
        // Read text from URL
        var textHeaders = await _client.ReadInStreamAsync(memoryStream);
        // After the request, get the operation location (operation ID)
        string operationLocation = textHeaders.OperationLocation;
        Thread.Sleep(2000);

        // <snippet_extract_response>
        // Retrieve the URI where the recognized text will be stored from the Operation-Location header.
        // We only need the ID and not the full URL
        const int numberOfCharsInOperationId = 36;
        string operationId = operationLocation.Substring(operationLocation.Length - numberOfCharsInOperationId);

        // Extract the text
        ReadOperationResult results;

        do
        {
            results = await _client.GetReadResultAsync(Guid.Parse(operationId));
        }
        while ((results.Status == OperationStatusCodes.Running ||
                results.Status == OperationStatusCodes.NotStarted));

        var readResult = new StringBuilder();

        var textUrlFileResults = results.AnalyzeResult.ReadResults;
        foreach (ReadResult page in textUrlFileResults)
        {
            foreach (Line line in page.Lines)
            {
                readResult.Append(line.Text + " ");
            }
        }

        return results.ToString();
    }
}