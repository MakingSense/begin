using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BeginMobile.Services.DTO
{
    public class Notification
    {
        [JsonProperty("notification_id")]
        public string NotificationId { set; get; }

        [JsonProperty("component")]
        public string Component { set; get; }

        [JsonProperty("action")]
        public string Action { set; get; }

        [JsonProperty("item_id")]
        public string ItemId { set; get; }

        [JsonProperty("date_notified")]
        public string DateNotified { set; get; }

        [JsonProperty("user")]
        public SubUser User { set; get; }

        [JsonProperty("group")]
        public Group Group { get; set; }
    }
}