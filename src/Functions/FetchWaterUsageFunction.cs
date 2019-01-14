using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using System.Linq;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using ServeUp.Models;
using ServeUp.System;
using ServeUp.Stacks;
using Microsoft.Extensions.Logging;

namespace ServeUp.Functions
{ 
    public class FetchWaterUsageFunction
    {
              
        [FunctionName("FetchWaterUsage")]
        public static async Task<IActionResult> Run(
            [HttpTriggerAttribute(AuthorizationLevel.Anonymous, "get", Route = "waterusage/{deviceId}/{sensorId}/{month}/{year}")] HttpRequest request, 
            ILogger logger, string deviceid, string sensorId, int month, int year)
        {
            
            var pipeLine = new DefualtPipeLine(logger);

            var waterUsageIdentifer = new WaterUsageIdentifier {
                DeviceId = deviceid,
                SensorId = sensorId,
                Month = month,
                Year = year
            };
            
            await pipeLine.ExecuteStack<FetchWaterUsageStack, WaterUsageIdentifier, WaterUsage>(waterUsageIdentifer);

            return pipeLine.Context.ToActionResult();
            
        }
    }
} 
        
