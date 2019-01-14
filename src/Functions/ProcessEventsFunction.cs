using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServeUp.Models;
using ServeUp.Stacks;
using ServeUp.System;

namespace ServeUp.Functions
{
    public class ProcessEventsFunction
    {
        public static async Task Run(EventData eventData, ILogger logger)
        {
            var telemetry = JsonConvert.DeserializeObject<WaterFlowTelemetry>(Encoding.UTF8.GetString(eventData.Body.Array), new JsonEpochConverter());
            var deviceId = eventData.Properties["iothub-connection-device-id"].ToString();

            var deviceTelemetry = new DeviceWaterFlowTelemetry
            {
                DeviceId = deviceId,
                Telemetry = telemetry
            };

            var pipeLine = new DefualtPipeLine(logger);
            
            await pipeLine.ExecuteStack<LogWaterFlowTelemetryStack, DeviceWaterFlowTelemetry>(deviceTelemetry);

            if(!pipeLine.Context.Success)
            {
                logger.LogInformation($"Exception running ProcessEventsFunction: {pipeLine.Context.Exception.ToString()}");
            }

            logger.LogInformation($"Event triggered with ml: {telemetry.Milliliters} {telemetry.Start.ToLongTimeString()}");
        }
    }
}