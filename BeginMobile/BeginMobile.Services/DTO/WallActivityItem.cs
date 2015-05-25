using Newtonsoft.Json;

namespace BeginMobile.Services.DTO
{
    public class WallActivityItem
    {
        [JsonProperty("item_id")]
        public string ItemId { set; get; }

        [JsonProperty("secondary_item_id")]
        public string SecondaryItemId { set; get; }

        [JsonProperty("component")]
        public string Component { set; get; }

        [JsonProperty("type")]
        public string Type { set; get; }

        [JsonProperty("date")]
        public string Date { set; get; }

        [JsonProperty("user")]
        public SubUser User { set; get; }

        [JsonProperty("user_1")]
        public SubUser User1 { set; get; }

        [JsonProperty("user_2")]
        public SubUser User2 { set; get; }

        [JsonProperty("event")]
        public ProfileEvent Event { set; get; }

        [JsonProperty("group")]
        public Group Group { set; get; }

        [JsonProperty("content")]
        public string Content { set; get; }
    }
}