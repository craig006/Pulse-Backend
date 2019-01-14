using System.Threading.Tasks;
using ServeUp.Models;
using ServeUp.Queries;

namespace ServeUp.Blocks
{
    public class FetchWaterUsageBlock
    {
        private readonly IFetchWaterUsageQuery _fetchWaterUsageQuery;

        public FetchWaterUsageBlock(IFetchWaterUsageQuery fetchWaterUsageQuery)
        {
            _fetchWaterUsageQuery = fetchWaterUsageQuery;
        }

        public async Task<WaterUsage> Begin(WaterUsageIdentifier waterUsageIdentifier)
        {
            var log = await _fetchWaterUsageQuery.Begin(waterUsageIdentifier);

            if (log == null)
            {
                log = new WaterUsage
                {
                    Identity = waterUsageIdentifier
                };
            }

            return log;
        }
    }
}