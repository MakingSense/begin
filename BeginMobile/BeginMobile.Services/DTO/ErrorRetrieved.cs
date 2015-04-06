using Newtonsoft.Json;

namespace BeginMobile.Services.DTO
{
    public class ErrorRetrieved
    {
        [JsonProperty("code")]
        public string Code { set; get; }

        [JsonProperty("label")]
        public string Label{ set; get; }
    }
}
