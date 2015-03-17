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
    public class ProfileManager
    {
        public ProfileInformation GetProfileInformation(string username)
        {
            using (var client = new HttpClient())
            {
                ProfileInformation profileInformation = null;

                client.BaseAddress = new Uri("http://104.236.207.173/");

                var content = new FormUrlEncodedContent(new[] 
                {
                    new KeyValuePair<string, string>("username", username),
                    
                });

                var response = client.PostAsync("api/index.php?/v1/profile", content).Result;
                var userJson = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    profileInformation = JsonConvert.DeserializeObject<ProfileInformation>(userJson);
                }

                return profileInformation;
            }
        }
    }
}
