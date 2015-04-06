using System.Collections.Generic;
using Newtonsoft.Json;

namespace BeginMobile.Services.DTO
{
    public class ProfileInformationMessages
    {
        [JsonProperty("id")]
        public int Id { set; get; }

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
        public List<Message> Messages { set; get; }

        public GroupingMessage GroupingMessage { set; get; }
    }
}
