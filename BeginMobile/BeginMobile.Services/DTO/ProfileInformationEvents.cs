using Newtonsoft.Json;

namespace BeginMobile.Services.DTO
{
    public class ProfileInformationEvents
    {
        [JsonProperty("id")]
        public string Id { set; get; }

        [JsonProperty("username")]
        public string Username { set; get; }

        [JsonProperty("nicename")]
        public string Nicename { set; get; }

        [JsonProperty("email")]
        public string Email { set; get; }

        [JsonProperty("url")]
        public string Url { set; get; }

        [JsonProperty("registered")]
        public string Registered { set; get; }

        [JsonProperty("displayname")]
        public string Displayname { set; get; }

        [JsonProperty("events")]
        public ProfileEvent[] Events { set; get; }
    }
}
