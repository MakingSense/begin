using Newtonsoft.Json;

namespace BeginMobile.Services.DTO
{
    public class UserActivity
    {
        [JsonProperty("user_id")]
        public int Id { set; get; }

        [JsonProperty("email")]
        public string Email { set; get; }

        [JsonProperty("username")]
        public string Username { set; get; }

        [JsonProperty("displayname")]
        public string DisplayName { set; get; }
    }
}