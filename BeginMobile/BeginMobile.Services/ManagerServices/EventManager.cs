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
    public class EventManager
    {
        private const string BaseAddress = "http://186.109.86.251:5432/";
        private const string SubAddress = "begin/api/v1/events";

        public EventManager() { }

        public List<ProfileEvent> GetEventsByParams(
            string authToken, 
            string name = null, 
            string cat = null, 
            string limit = null
            )
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("authtoken", authToken);

                client.BaseAddress = new Uri(BaseAddress);

                var urlGetParams = "?q=" + name + "&cat=" + cat + "&limit=" + limit;

                var response = client.GetAsync(SubAddress + urlGetParams).Result;
                var userJson = response.Content.ReadAsStringAsync().Result;

                List<ProfileEvent> profileEvents;

                if (response.IsSuccessStatusCode)
                {
                    profileEvents = JsonConvert.DeserializeObject<List<ProfileEvent>>(userJson);
                }
                else
                {
                    profileEvents = JsonConvert.DeserializeObject<List<ProfileEvent>>(userJson);
                }

                return profileEvents;
            }
        }

        public ProfileEvent GetEventById(string authToken, string eventId)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("authtoken", authToken);

                client.BaseAddress = new Uri(BaseAddress);

                var urlId = "/"+eventId;

                var response = client.GetAsync(SubAddress + urlId).Result;
                var userJson = response.Content.ReadAsStringAsync().Result;

                ProfileEvent profileEvent;

                if (response.IsSuccessStatusCode)
                {
                    profileEvent = JsonConvert.DeserializeObject<ProfileEvent>(userJson);
                }
                else
                {
                    profileEvent = JsonConvert.DeserializeObject<ProfileEvent>(userJson);
                }

                return profileEvent;
            }
        }

        private string BuildUrlParams(string name, string cat, string limit)
        {
            var resultUrl = "";

            if (!(string.IsNullOrEmpty(name) && string.IsNullOrEmpty(cat) && string.IsNullOrEmpty(limit)))
            {
                resultUrl = "?q=" + name + "&cat=" + cat + "&limit=" + limit;
            }
            else if (!(string.IsNullOrEmpty(name) && string.IsNullOrEmpty(cat)))
            {
                resultUrl = "?q=" + name + "&cat=" + cat;
            }
            else if (!(string.IsNullOrEmpty(cat) && string.IsNullOrEmpty(limit)))
            {
                resultUrl = "?cat=" + cat + "&limit=" + limit;
            }
            else if (!(string.IsNullOrEmpty(name) && string.IsNullOrEmpty(limit)))
            {
                resultUrl = "?q=" + name + "&limit=" + limit;
            }
            else if (!string.IsNullOrEmpty(name))
            {
                resultUrl = "?q=" + name;
            }
            else if (!string.IsNullOrEmpty(cat))
            {
                resultUrl = "?cat=" + cat;
            }
            else if (!string.IsNullOrEmpty(limit))
            {
                resultUrl = "?limit=" + limit;
            }

            return resultUrl;
        }

    }
}
