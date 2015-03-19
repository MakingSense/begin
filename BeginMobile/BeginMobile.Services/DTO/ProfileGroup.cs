using Newtonsoft.Json;

namespace BeginMobile.Services.DTO
{
    public class ProfileGroup
    {
        [JsonProperty("group_id")]
        public int GroupId { set; get; }

        [JsonProperty("creator_id")]
        public int CreatorId { set; get; }

        [JsonProperty("name")]
        public string Name { set; get; }

        [JsonProperty("description")]
        public string Description { set; get; }

        [JsonProperty("status")]
        public string Status { set; get; }

        [JsonProperty("date_created")]
        public string DateCreated { set; get; }

        [JsonProperty("is_admin")]
        public int IsAdmin { set; get; }

        [JsonProperty("creator")]
        public User Creator { set; get; }

    }
}
