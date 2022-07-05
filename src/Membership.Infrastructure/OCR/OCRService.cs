using Membership.Core.Abstractions;
using Membership.Core.Repositories.Commons;
using Membership.Infrastructure.OCR;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using OcrResult = Membership.Core.Entities.Commons.OcrResult;

internal sealed class OcrService : IOcrService
{
    private readonly IOcrResultRepository _repository;
    private readonly IClock _clock;
    private readonly string _subscriptionKey = "01b9a5cdb3d047a6b2689ef7cfd076a4";
    private readonly string _endpoint = "https://membership-app.cognitiveservices.azure.com/";
    private readonly ComputerVisionClient _client;
    
    public OcrService(IOcrResultRepository repository, IClock clock)
    {
        repository = repository;
        _clock = clock;
        _client = Authenticate(_endpoint, _subscriptionKey);
    }
    
    public async Task<OcrResult> ReadData(string fileInfo)
    {
           // Read text from URL
        var textHeaders = await _client.ReadInStreamAsync(File.OpenRead(fileInfo));
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

        var result = OcrResult.Create(Guid.NewGuid(), null, null, null );
        {

        };

        var textUrlFileResults = results.AnalyzeResult.ReadResults;
        foreach (ReadResult page in textUrlFileResults)
        {
            foreach (Line line in page.Lines)
            {
                Console.WriteLine(line.Text);
            }
        }

        return result;
    }
    
    private ComputerVisionClient Authenticate(string endpoint, string key)
    {
        ComputerVisionClient client =
            new ComputerVisionClient(new ApiKeyServiceClientCredentials(key))
                { Endpoint = endpoint };
        return client;
    }
}