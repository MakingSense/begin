using Newtonsoft.Json;

namespace BeginMobile.Services.DTO
{
    public class LoginUser
    {
        [JsonProperty("authtoken")]
        public string AuthToken { set; get; }

        [JsonProperty("me")]
        public User User { set; get; }

        [JsonProperty("avatar")]
        public string Avatar { set; get; }

        [JsonProperty("errors")]
        public ErrorRetrieved[] Errors { set; get; }
    }
}
