using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BeginMobile.Services.DTO
{
    public class ProfileNotification: BaseServiceError
    {
        [JsonProperty("notifications")]
        public List<Notification> Notifications { set; get; }

        [JsonProperty("unread_count")]
        public string UnreadCount { set; get; }
    }
}