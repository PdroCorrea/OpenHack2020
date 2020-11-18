using BFYOCSolutions.NewFolder;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Functions.Users
{
    public class UserApi
    {

        private const string BaseUrl = "https://serverlessohuser.trafficmanager.net/api/";

        public static async Task<User> GetUserByIdAsync(Guid userId)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(BaseUrl);
                var response = await httpClient.GetStringAsync($"GetUser?userId={userId}");
                var user = JsonConvert.DeserializeObject<User>(response);
                return user;
            }
        }
    }
}