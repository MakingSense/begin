using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BeginMobile.Services.DTO
{
    public class Wall
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

        [JsonProperty("user_1")]
        public SubUser User1 { set; get; }

        [JsonProperty("user_2")]
        public SubUser User2 { set; get; }
    }
}
