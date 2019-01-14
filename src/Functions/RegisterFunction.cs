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
    public class RegisterFunction
    {
              
        [FunctionName("RegisterUser")]
        public static async Task<IActionResult> Run(
            [HttpTriggerAttribute(AuthorizationLevel.Anonymous, "post", Route = "UserRegistration")] HttpRequest request, ILogger logger)
        {

            logger.LogTrace("Hello world");
            logger.LogCritical("Hello world");
            
            var userRegistration = JsonConvert.DeserializeObject<UserRegistration>(await request.ReadAsStringAsync());

            var pipeLine = new DefualtPipeLine(logger);
            
            await pipeLine.ExecuteStack<RegisterUserStack, UserRegistration>(userRegistration);

            return pipeLine.Context.ToActionResult();
        }
    }
} 
        
