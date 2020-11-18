using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BFYOCSolutions.Products
{
    class ProductsApi
    {
        private const string BaseUrl = "https://serverlessohuser.trafficmanager.net/api/";

        public static async Task<Product> GetProductByIdAsync(Guid productId)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(BaseUrl);
                var response = await httpClient.GetStringAsync($"GetProduct?productId={productId}");
                var user = JsonConvert.DeserializeObject<Product>(response);
                return user;
            }
        }
    }
}
