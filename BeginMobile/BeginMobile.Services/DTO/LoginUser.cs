using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BeginMobile.Services.DTO
{
    public class LoginUser
    {
        [JsonProperty("authtoken")]
        public string Authtoken { set; get; }

        [JsonProperty("me")]
        public User User { set; get; }

        [JsonProperty("avatar")]
        public string Avatar { set; get; }
    }
}
