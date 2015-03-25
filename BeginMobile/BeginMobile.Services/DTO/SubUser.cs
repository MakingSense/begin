using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BeginMobile.Services.DTO
{
    public class SubUser
    {
        [JsonProperty("user_id")]
        public string UserId { set; get; }

        [JsonProperty("email")]
        public string Email { set; get; }

        [JsonProperty("username")]
        public string Username { set; get; }

        [JsonProperty("display_name")]
        public string DisplayName { set; get; }
    }
}
