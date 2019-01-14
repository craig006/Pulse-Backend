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
    public class AuthenticateFunction
    {
              
        [FunctionName("Authenticate")]
        public static async Task<IActionResult> Run(
            [HttpTriggerAttribute(AuthorizationLevel.Anonymous, "post", Route = "Authenticate")] HttpRequest request, 
            ILogger logger)
        {
            var credentials = JsonConvert.DeserializeObject<Credentials>(await request.ReadAsStringAsync());

            var pipeLine = new DefualtPipeLine(logger);
            
            await pipeLine.ExecuteStack<AuthenticateStack, Credentials, string>(credentials);

            return pipeLine.Context.ToActionResult();
            
        }
    }
} 
        
