using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace EclipseBattleForecaster.Functions
{
    public static class Session
    {
        [Function("Session")]
        [CosmosDBOutput(databaseName: "cosmos-ebf", collectionName: "container-ebf", ConnectionStringSetting = "CosmosDbConnectionString")]
        public static async Task<dynamic> Run( [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "session/{id:int?}")] HttpRequestData req, FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("GetSpecies");
            logger.LogInformation($"Retrieving a session {req.Method}, {executionContext.BindingContext.BindingData["id"]}");

            return new { Title = "Hello", Content = await new StreamReader(req.Body).ReadToEndAsync() };
        }
    }
}
