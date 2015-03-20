using Newtonsoft.Json;

namespace BeginMobile.Services.DTO
{
    public class UserActivity
    {
        [JsonProperty("user_id")]
        public int UserId { set; get; }

        [JsonProperty("email")]
        public string Email { set; get; }

        [JsonProperty("username")]
        public string UserName { set; get; }

        [JsonProperty("display_name")]
        public string DisplayName { set; get; }
    }
}