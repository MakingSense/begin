using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BeginMobile.Services.DTO
{
    public class ThreadMessages
    {
        [JsonProperty("thread_id")]
        public int ThreadId { set; get; }

        [JsonProperty("date_sent")]
        public string DateSent { set; get; }

        [JsonProperty("unread")]
        public string Unread { set; get; }

        [JsonProperty("messages")]
        public List<Message> Messages { set; get; }
    }
}
