internal sealed class BufferedFileUploadLocalService : IOCRService
{
    private readonly IOCRResultRepository _repository;
    private readonly IClock _clock;
    
    public BufferedFileUploadLocalService(IOCRResultRepository repository, IClock clock)
    {
        repository = repository;
        _clock = clock;
    }
    
    public async Task<Guid?> ReadData(string fileInfo)
    {
           // Read text from URL
        var textHeaders = await client.ReadInStreamAsync(File.OpenRead(localFile));
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
        Console.WriteLine($"Reading text from local file {Path.GetFileName(localFile)}...");
        Console.WriteLine();
        do
        {
            results = await client.GetReadResultAsync(Guid.Parse(operationId));
        }
        while ((results.Status == OperationStatusCodes.Running ||
            results.Status == OperationStatusCodes.NotStarted));
        // </snippet_extract_response>

        // <snippet_extract_display>
        // Display the found text.
        Console.WriteLine();
        var textUrlFileResults = results.AnalyzeResult.ReadResults;
        foreach (ReadResult page in textUrlFileResults)
        {
            foreach (Line line in page.Lines)
            {
                Console.WriteLine(line.Text);
            }
        }
    }
}