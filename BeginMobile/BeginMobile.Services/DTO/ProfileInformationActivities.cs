using System.Collections.Generic;
using Newtonsoft.Json;

namespace BeginMobile.Services.DTO
{
    public class ProfileInformationActivities
    {
        [JsonProperty("id")]
        public string Id { set; get; }

        [JsonProperty("username")]
        public string UserName { set; get; }

        [JsonProperty("email")]
        public string Email { set; get; }

        [JsonProperty("url")]
        public string Url { set; get; }

        [JsonProperty("registered")]
        public string Registered { set; get; }

        [JsonProperty("name_surname")]
        public string NameSurname { set; get; }

        [JsonProperty("activities")]
        public List<ProfileActivity> Activities { set; get; }
    }
}