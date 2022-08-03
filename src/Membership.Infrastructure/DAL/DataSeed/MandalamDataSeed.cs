using Newtonsoft.Json;

namespace Membership.Infrastructure.DAL.DataSeed;

internal sealed class MandalamDataSeed
{
    public static IEnumerable<MandalamSeedContract> GetSeedData()
    {
        string fileName = "Mandalam.json";
        string filePath = "./DataSeed/";
        
        filePath = Path.Combine(filePath, fileName);
        
        if (!File.Exists(filePath))
        {
            return null;
        }
        var json = File.ReadAllText(filePath);
        var datas = JsonConvert.DeserializeObject<List<MandalamSeedContract>>(json);
        return datas;
    }
}

internal sealed class MandalamSeedContract
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid DistrictId { get; set; }
}