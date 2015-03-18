using Newtonsoft.Json;

namespace BeginMobile.Services.DTO
{
    public class Activity
    {
        [JsonProperty("user")]
        public UserActivity User { get; set; }

        [JsonProperty("item_id")]
        public int ItemId { get; set; }

        [JsonProperty("component")]
        public string Component { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }
    }
}