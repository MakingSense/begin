using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BeginMobile.Services.DTO
{
    public class User
    {
        [JsonProperty("Id")]
        public int Id { set; get; }

        [JsonProperty("username")]
        public string Userame { set; get; }

        [JsonProperty("nicename")]
        public string Nicename { set; get; }

        [JsonProperty("email")]
        public string Email { set; get; }

        [JsonProperty("url")]
        public string Url { set; get; }

        [JsonProperty("registered")]
        public string Registered { set; get; }

        [JsonProperty("displayname")]
        public string Displayname { set; get; }

        [JsonProperty("firstname")]
        public string Firstname { set; get; }

        [JsonProperty("lastname")]
        public string Lastname { set; get; }

        [JsonProperty("nickname")]
        public string Nickname { set; get; }

        [JsonProperty("description")]
        public string Description { set; get; }

        [JsonProperty("capabilities")]
        public Capability Capabilities { set; get; }
    }
}
