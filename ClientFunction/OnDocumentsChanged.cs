using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace SignalRFlights
{
    public static class OnDocumentChanged
    {
        [FunctionName("OnDocumentsChanged")]
        public static async Task Run(
            [CosmosDBTrigger("TestDatabase", "TestContainer", ConnectionStringSetting = "AzureWebJobsCosmosDBConnectionString", 
            LeaseCollectionName = "leases", CreateLeaseCollectionIfNotExists = true)]
                IEnumerable<object> updatedData,
            [SignalR(HubName = "testhub")] IAsyncCollector<SignalRMessage> signalRMessages,
            ILogger log)
        {
            foreach (var newData in updatedData)
            {
                await signalRMessages.AddAsync(new SignalRMessage
                {
                    Target = "dataUpdated",
                    Arguments = new[] { newData }
                });
            }
        }
    }
}
