using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace ClientFunction
{
    public static class GetData
    {
        [FunctionName("GetData")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous)] HttpRequest req,
            [CosmosDB("TestDatabase", "TestContainer", ConnectionStringSetting = "AzureWebJobsCosmosDBConnectionString")]
                IEnumerable<object> runningData,
            ILogger log)
        {
            return new OkObjectResult(runningData);
        }
    }
}
