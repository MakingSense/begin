using Newtonsoft.Json;

namespace BeginMobile.Services.DTO
{
    public class ProfileEvent
    {
        [JsonProperty("event_id")]
        public string EventId { set; get; }

        [JsonProperty("owner_id")]
        public string OwnerId { set; get; }

        [JsonProperty("status")]
        public string Status { set; get; }

        [JsonProperty("name")]
        public string Name { set; get; }

        [JsonProperty("start_date")]
        public string StartDate { set; get; }

        [JsonProperty("start_time")]
        public string StartTime { set; get; }

        [JsonProperty("end_date")]
        public string EndDate { set; get; }

        [JsonProperty("end_time")]
        public string EndTime { set; get; }

        [JsonProperty("all_day")]
        public string AllDay { set; get; }

        [JsonProperty("content")]
        public string Content { set; get; }

        [JsonProperty("rsvp")]
        public string Rsvp { set; get; }

        [JsonProperty("rsvp_spaces")]
        public string RsvpSpaces { set; get; }

        [JsonProperty("spaces")]
        public string Spaces { set; get; }

        [JsonProperty("private")]
        public string Private { set; get; }

        [JsonProperty("owner")]
        public string User { set; get; }
    }
}