using Newtonsoft.Json;

namespace BeginMobile.Services.DTO
{
    public class ProfileActivity
    {
        [JsonProperty("user")]
        public UserActivity UserActivity { get; set; }

        [JsonProperty("item_id")]
        public int ItemId { get; set; }

        [JsonProperty("component")]
        public string Component { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }
    }
}