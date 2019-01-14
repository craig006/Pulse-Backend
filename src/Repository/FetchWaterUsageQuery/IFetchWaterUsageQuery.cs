using System.Threading.Tasks;
using ServeUp.Models;

namespace ServeUp.Queries
{
    public interface IFetchWaterUsageQuery
    {
        Task<WaterUsage> Begin(WaterUsageIdentifier waterUsageIdentifier);
    }
}