using Newtonsoft.Json;

namespace Membership.Infrastructure.DAL.DataSeed;

internal sealed class MandalamDataSeed
{
    public static IEnumerable<MandalamSeedContract> GetSeedData()
    {
        string path = Directory.GetCurrentDirectory();
        string filePath = path +  @"\DataSeed\Mandalam.json";
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