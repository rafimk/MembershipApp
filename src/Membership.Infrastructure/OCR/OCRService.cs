using System.Text;
using Membership.Core.Abstractions;
using Membership.Core.Consts;
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
    
    public async Task<OcrResult> ReadData(string fileInfo, Guid? frontPageId, Guid? lastPageId)
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

        var readResult = new StringBuilder();

        var textUrlFileResults = results.AnalyzeResult.ReadResults;
        foreach (ReadResult page in textUrlFileResults)
        {
            foreach (Line line in page.Lines)
            {
                readResult.Append(line.Text + " ");
            }
        }
        
        string eidNo = "";
        string name = "";
        var dob = "";
        
        var finalResult = readResult.ToString().RemoveSpecialCharacters();

        int firstStringPositionForEid = finalResult.IndexOf("ID Number ");

        if (firstStringPositionForEid > 0)
        {
            eidNo = finalResult.Substring(firstStringPositionForEid + 10, 18);
        }

        int firstStringPositionForName = finalResult.IndexOf("Name:");    
        int secondStringPositionForName = finalResult.IndexOf(":  Nationality:");

        if (firstStringPositionForName > 0)
        {
            name = finalResult.Substring(firstStringPositionForName + 5,
                secondStringPositionForName - (firstStringPositionForName + 5));
        }

        var split = name.Split(":");
        
        if (split.Length > 1)
        {
            name = split[0];
            dob = split[1].Substring(0, 8);
        }

        int firstStringPositionForDob = finalResult.IndexOf(" Date of Birth");

        if (firstStringPositionForDob > 0) 
        {
            dob = finalResult.Substring(firstStringPositionForDob - 8, 8);
        }
        
        var result = OcrResult.Create(Guid.NewGuid(),  frontPageId, lastPageId, eidNo, name, new DateOnly(), 
            new DateOnly(), "", CardType.New, DateTime.UtcNow, Guid.NewGuid());

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