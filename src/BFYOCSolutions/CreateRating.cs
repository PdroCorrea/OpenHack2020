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
using Functions.Users;
using System.Net.Http;
using System.Net;
using System.Text;
using BFYOCSolutions.Products;

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
    5) Use a data service to store the ratings information to the backend

 */


namespace BFYOCSolutions
{
    public static class CreateRating
    {
        [FunctionName("CreateRating")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequestMessage req,
            [CosmosDB(databaseName: "BFYOC", collectionName: "Ratings", ConnectionStringSetting = "CosmosDBConnection")] IAsyncCollector<RatingOutputPayload> document,
            ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");


                RatingInputPayload data = JsonConvert.DeserializeObject<RatingInputPayload>(await req.Content.ReadAsStringAsync());

                // Validate the rating: must be 0-5
                if (data.rating < 0 || data.rating > 5)
                {
                    string invalidRatingMessage = "You have entered an invalid rating number. Please enter a number from 0-5";
                    //return req.CreateErrorResponse(HttpStatusCode.BadRequest, invalidRatingMessage);
                    return new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(invalidRatingMessage), Encoding.UTF8, "application/json"),
                    };
                }

                // map the request body to the Rating Output payload if rating is valid
                RatingOutputPayload output = new RatingOutputPayload()
                {
                    id = Guid.NewGuid(),
                    userId = data.userId,
                    productId = data.productId,
                    timestamp = DateTime.UtcNow,
                    locationName = data.locationName,
                    rating = data.rating,
                    userNotes = data.userNotes
                };

                try
                {
                    await UserApi.GetUserByIdAsync(data.userId);
                }
                catch (Exception e)
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(e), Encoding.UTF8, "application/json"),
                    };
                }

                try
                {
                    await ProductsApi.GetProductByIdAsync(data.productId);
                }
                catch (Exception e)
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(e), Encoding.UTF8, "application/json"),
                    };
                }

                //await document.AddAsync(output);
                await document.AddAsync(output);
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(output), Encoding.UTF8, "application/json"),
                };
                
            }
            catch (Exception ex)
            {
                //return req.CreateResponse(HttpStatusCode.BadRequest, $"error saving to cosmos or whatever here check it => {ex.ToString()} ");
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject($"error saving to cosmos or whatever here check it => {ex.ToString()} "), Encoding.UTF8, "application/json"),
                };
            }
        }
    }
}
