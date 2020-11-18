using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using BFYOCSolutions.Models;

/* Challenge #3
    POST Azure Function
    Input:
          {
            "userId": "cc20a6fb-a91f-4192-874d-132493685376",
            "productId": "4c25613a-a3c2-4ef3-8e02-9c335eb23204",
            "locationName": "Sample ice cream shop",
            "rating": 5,
            "userNotes": "I love the subtle notes of orange in this ice cream!"
           }
    
    Output: 
        {
          "id": "79c2779e-dd2e-43e8-803d-ecbebed8972c",
          "userId": "cc20a6fb-a91f-4192-874d-132493685376",
          "productId": "4c25613a-a3c2-4ef3-8e02-9c335eb23204",
          "timestamp": "2018-05-21 21:27:47Z",
          "locationName": "Sample ice cream shop",
          "rating": 5,
          "userNotes": "I love the subtle notes of orange in this ice cream!"
        }

    TODO
    1) Validate userId and productId with external api
    2) Add Id : GUID
    3) Add timestamp (DateTime UTC)
    4) Validation for rating 0-5
    5) Use a data service to store the ratings information to the backend
    6) Return JSON payload with generated id and timestamp

 */


namespace BFYOCSolutions
{
    public static class CreateRating
    {
        [FunctionName("CreateRating")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");


            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            RatingInputPayload data = (RatingInputPayload)JsonConvert.DeserializeObject(requestBody);



            string responseMessage = string.IsNullOrEmpty(null)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, . This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}
