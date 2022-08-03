using System.Text;
using Membership.Core.Abstractions;
using Membership.Core.Consts;
using Membership.Core.Repositories.Commons;
using Membership.Infrastructure.DAL.Exceptions;
using Membership.Infrastructure.Exceptions;
using Membership.Infrastructure.OCR;
using Membership.Infrastructure.OCR.Consts;
using Membership.Infrastructure.OCR.Policies;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

internal sealed class OcrService : IOcrService
{
    private readonly IEnumerable<ICardReadPolicy> _policies;
    private readonly IOcrResultRepository _repository;
    private readonly IClock _clock;
    private readonly string _subscriptionKey = "d4f537561bdd405489046ac0e633cdc0";
    private readonly string _endpoint = "https://uaekmcc.cognitiveservices.azure.com/";
    private readonly ComputerVisionClient _client;
    
    public OcrService(IEnumerable<ICardReadPolicy> policies, IOcrResultRepository repository, IClock clock)
    {
        _policies = policies;
        _repository = repository;
        _clock = clock;
        _client = Authenticate(_endpoint, _subscriptionKey);
    }
    
    public async Task<OcrData> ReadData(string fileInfo, Guid userId)
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
        
        var finalResult = readResult.ToString().RemoveSpecialCharacters();
        
        CardSide currentCardSide = null;
        CardType currentCardType = CardType.New;
        
        // Check if the card is new or old 
        if (finalResult.IndexOf("Name:") > 0 && finalResult.IndexOf("Expiry Date") > 0)
        {
            currentCardSide = CardSide.NewCardFrontSide();
            currentCardType = CardType.New;
        }

        if (finalResult.IndexOf("Name:") > 0 && finalResult.IndexOf("Expiry Date") == -1)
        {
            currentCardSide = CardSide.OldCardFrontSide();
            currentCardType = CardType.Old;
        }

        if (finalResult.IndexOf("Issuing Place:") > 0)
        {
            currentCardSide = CardSide.NewCardBackSide();
            currentCardType = CardType.New;
        }
        
        if (finalResult.IndexOf("Sex:") > 0 && finalResult.IndexOf("Name:") == -1 && finalResult.IndexOf("No signature") > 0)
        {
            currentCardSide = CardSide.OldCardBackChildSide();
            currentCardType = CardType.Old;
        }

        if (finalResult.IndexOf("Sex:") > 0 && finalResult.IndexOf("Name:") == -1 && finalResult.IndexOf("No signature") == -1)
        {
            currentCardSide = CardSide.OldCardBackSide();
            currentCardType = CardType.Old;
        }
        
        var policy = _policies.SingleOrDefault(x => x.CanBeApplied(currentCardSide));

        if (policy is null)
        {
            throw new NoCardPolicyFoundException(currentCardSide.ToString());
        }
 
        return policy.ReadData(currentCardSide, finalResult);
    }
    
    private ComputerVisionClient Authenticate(string endpoint, string key)
    {
        ComputerVisionClient client =
            new ComputerVisionClient(new ApiKeyServiceClientCredentials(key))
                { Endpoint = endpoint };
        return client;
    }

}