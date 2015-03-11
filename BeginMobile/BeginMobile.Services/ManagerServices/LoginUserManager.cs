using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BeginMobile.Services.DTO;
using Newtonsoft.Json;

namespace BeginMobile.Services.ManagerServices
{
    public class LoginUserManager
    {
        public LoginUser Login(string username, string password)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://104.236.207.173/");

                var content = new FormUrlEncodedContent(new[] 
                {
                    new KeyValuePair<string, string>("username", username),
                    new KeyValuePair<string, string>("password", password)
                });

                var response = client.PostAsync("api/index.php?/v1/login", content).Result;
                var userJson = response.Content.ReadAsStringAsync().Result;

                var loginUser = JsonConvert.DeserializeObject<LoginUser>(userJson);

                return loginUser;
            }
        }
    }
}
