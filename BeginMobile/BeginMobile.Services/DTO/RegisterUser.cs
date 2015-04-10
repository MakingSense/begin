using Newtonsoft.Json;

namespace BeginMobile.Services.DTO
{
    public class RegisterUser: BaseServiceError
    {
        [JsonProperty("authtoken")]
        public string AuthToken { set; get; }

        [JsonProperty("me")]
        public User User { set; get; }
    }
}