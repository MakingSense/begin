using System.Collections.Generic;
using Newtonsoft.Json;

namespace BeginMobile.Services.DTO
{
    public class ProfileContacts: BaseServiceError
    {
        [JsonProperty("id")]
        public int Id { set; get; }

        [JsonProperty("username")]
        public string UserName { set; get; }

        [JsonProperty("nicename")]
        public string NiceName { set; get; }

        [JsonProperty("email")]
        public string Email { set; get; }

        [JsonProperty("url")]
        public string Url { set; get; }

        [JsonProperty("registered")]
        public string Registered { set; get; }

        [JsonProperty("displayname")]
        public string DisplayName { set; get; }

        [JsonProperty("contacts")]
        public List<User> Contacts { set; get; }

    }
}