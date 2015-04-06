using Newtonsoft.Json;

namespace BeginMobile.Services.DTO
{
    public class Capability
    {
        [JsonProperty("vendor")]
        public bool Vendor { set; get; }

        [JsonProperty("bbp_participant")]
        public bool PbpParticipant { set; get; }
    }
}