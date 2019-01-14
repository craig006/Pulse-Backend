using System.Threading.Tasks;
using ServeUp.Blocks;
using ServeUp.Models;
using ServeUp.Queries;
using ServeUp.System;

namespace ServeUp.Stacks
{
    public class LogWaterFlowTelemetryStack : IBeginnable<DeviceWaterFlowTelemetry>
    {
        private readonly FetchWaterUsageBlock _fetchWaterFlowByMonthBlock;
        private readonly IUpsertQuery<WaterUsage> _upsertQuery;

        public LogWaterFlowTelemetryStack(FetchWaterUsageBlock fetchWaterFlowByMonthBlock, IUpsertQuery<WaterUsage> upsertQuery)
        {
            _upsertQuery = upsertQuery;
            _fetchWaterFlowByMonthBlock = fetchWaterFlowByMonthBlock;
        }

        public async Task Begin(DeviceWaterFlowTelemetry deviceFlowTelemetry)
        {
            var start = deviceFlowTelemetry.Telemetry.Start.AddHours(2); //GMT+2

            var waterUsageIdentifier = new WaterUsageIdentifier {
                DeviceId = deviceFlowTelemetry.DeviceId,
                SensorId = deviceFlowTelemetry.Telemetry.SensorId,
                Month = start.Month,
                Year = start.Year
            };

            var flowLog = await _fetchWaterFlowByMonthBlock.Begin(waterUsageIdentifier);

            flowLog.AddReading(start.Day, deviceFlowTelemetry.Telemetry.Milliliters);

            await _upsertQuery.Upsert(flowLog);
        }
    }


}