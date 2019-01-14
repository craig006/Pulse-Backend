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


namespace ServeUp.Functions
{ 
    public class Echo
    {              
        [FunctionName("Echo")]
        public static async Task<IActionResult> Run([HttpTriggerAttribute(AuthorizationLevel.Anonymous, "get", Route = "name/{name}/sname/{sname}")] HttpRequest req, string name, string sname)
        {
            return new OkObjectResult($"Hello {name} {sname}");
        }
    }
} 
        
