using System;
using System.Net.Http;
using BeginMobile.Services.DTO;
using Newtonsoft.Json;

namespace BeginMobile.Services.ManagerServices
{
    public class ProfileManager
    {
        public ProfileInfoUser GetProfileInformation(string username, string authToken)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authtoken", authToken);
                ProfileInfoUser profileInfoUser = null;

                client.BaseAddress = new Uri("http://104.236.207.173/");

                var response = client.GetAsync("api/index.php?/v1/profile/" + username).Result;

                if (response.IsSuccessStatusCode)
                {
                    var userJson = response.Content.ReadAsStringAsync().Result;
                    profileInfoUser = JsonConvert.DeserializeObject<ProfileInfoUser>(userJson);
                }
                return profileInfoUser;
            }
        }
    }
}