using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using BFYOCSolutions.Ratings;
using System.Collections.Generic;

namespace BFYOCSolutions
{
    public static class GetRatings
    {
        [FunctionName("GetRatings")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]
                HttpRequest req,
            [CosmosDB(
                databaseName: "BFYOC",
                collectionName: "Ratings",
                ConnectionStringSetting = "CosmosDBConnection",
                SqlQuery = "SELECT top 2 * FROM Ratings")]
                IEnumerable<RatingOutputPayload> ratings,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            foreach (RatingOutputPayload rating in ratings)
            {
                log.LogInformation(rating.userId.ToString());
            }
            return new OkObjectResult(ratings);
        }


    }
}
