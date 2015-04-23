using Newtonsoft.Json;

namespace BeginMobile.Services.DTO
{
    public class LoginUser: BaseServiceError
    {
        [JsonProperty("authtoken")]
        public string AuthToken { set; get; }

        [JsonProperty("me")]
        public User User { set; get; }


    }
}
