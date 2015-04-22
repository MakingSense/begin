using Newtonsoft.Json;

namespace BeginMobile.Services.DTO
{
    public class User
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

        [JsonProperty("firstname")]
        public string FirstName { set; get; }

        [JsonProperty("lastname")]
        public string Lastname { set; get; }

        [JsonProperty("nickname")]
        public string NickName { set; get; }

        [JsonProperty("description")]
        public string Description { set; get; }

        [JsonProperty("capabilities")]
        public Capability Capabilities { set; get; }

        [JsonProperty("name_surname")]
        public string NameSurname { set; get; }

        [JsonProperty("name_username")]
        public string NameUsername { set; get; }

        [JsonProperty("is_admin")]
        public string IsAdmin { set; get; }

        [JsonProperty("relationship")]
        public string Relationship { set; get; }

        [JsonProperty("is_online")]
        public bool IsOnline { set; get; }

        [JsonProperty("profession")]
        public string Profession { set; get; }
    }
}