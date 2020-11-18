using System;
using System.Collections.Generic;
using BFYOCSolutions.Ratings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace BFYOCSolutions
{
    public static class GetRating
    {
        [FunctionName("GetRating")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post",
                Route = "rating/{id}")]HttpRequest req,
            [CosmosDB(
                databaseName: "BFYOC",
                collectionName: "Ratings",
                ConnectionStringSetting = "CosmosDBConnection",
                Id = "{id}",
                PartitionKey = "{partitionKey}")] RatingOutputPayload rating,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (rating == null)
            {
                log.LogInformation($"Rating item not found");
            }
            else
            {
                log.LogInformation($"Found Rating item, User ID={rating.userId}");
            }
            return new OkResult();
        }
    }
}
