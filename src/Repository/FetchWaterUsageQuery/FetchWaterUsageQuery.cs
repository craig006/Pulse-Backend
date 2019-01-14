using System.Threading.Tasks;
using ServeUp.Models;
using System.Linq;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Extensions.Logging;
using ServeUp.Database;

namespace ServeUp.Queries
{
    public class FetchWaterUsageQuery : Query<WaterUsage>, IFetchWaterUsageQuery
    {
        private readonly ILogger _logger;

        public FetchWaterUsageQuery(DatabaseConnector databaseConnector, ILoggerFactory loggerFcatory): base(databaseConnector) 
        {
            _logger = loggerFcatory.CreateLogger<FetchUserByContactDetailsQuery>();
        }

        public async Task<WaterUsage> Begin(WaterUsageIdentifier waterUsageIdentifier)
        {
            var query = BaseQuery.Where(l => l.Identity.DeviceId == waterUsageIdentifier.DeviceId && l.Identity.SensorId == waterUsageIdentifier.SensorId && l.Identity.Month == waterUsageIdentifier.Month && l.Identity.Year == waterUsageIdentifier.Year).Take(1);
            var result = await query.AsDocumentQuery().ToListAsync();
            _logger.LogInformation($"FetchWaterFlowLogMonthQuery charge: ${result.RequestCharge}");
            return result.FirstOrDefault();
        }
    }
}