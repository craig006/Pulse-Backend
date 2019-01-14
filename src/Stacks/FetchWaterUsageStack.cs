using System.Threading.Tasks;
using ServeUp.Blocks;
using ServeUp.Models;
using ServeUp.System;

namespace ServeUp.Stacks
{
    public class FetchWaterUsageStack: IBeginnableReturn<WaterUsageIdentifier, WaterUsage>
    {
        private readonly FetchWaterUsageBlock _fetchWaterUsageBlock;

        public FetchWaterUsageStack(FetchWaterUsageBlock fetchWaterUsageBlock)
        {
            _fetchWaterUsageBlock = fetchWaterUsageBlock;
        }
    
        public async Task<WaterUsage> Begin(WaterUsageIdentifier waterUsageIdentifier)
        {
            return await _fetchWaterUsageBlock.Begin(waterUsageIdentifier);
        }
    }
}