using Newtonsoft.Json;

namespace BeginMobile.Services.DTO
{
    public class ChangePassword
    {
        [JsonProperty("errors")]
        public ErrorRetrieved[] Errors { set; get; }
    }
}