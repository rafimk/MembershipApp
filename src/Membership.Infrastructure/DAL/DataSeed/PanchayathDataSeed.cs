using Newtonsoft.Json;

namespace Membership.Infrastructure.DAL.DataSeed;

internal sealed class PanchayathDataSeed
{
    public static IEnumerable<PanchayathSeedContract> GetSeedData()
    {
        string path = Directory.GetCurrentDirectory();
        string filePath = path +  @"\DataSeed\Panchayath.json";
        if (!File.Exists(filePath))
        {
            return null;
        }
        var json = File.ReadAllText(filePath);
        var datas = JsonConvert.DeserializeObject<List<PanchayathSeedContract>>(json);
        return datas;
    }
}

internal sealed class PanchayathSeedContract
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid MandalamId { get; set; }
}