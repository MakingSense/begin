using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BeginMobile.Services.DTO
{
   public class ProfileInformation
    {
        [JsonProperty("authtoken")]
        public string AuthToken { set; get; }

        [JsonProperty("me")]
        public ProfileInfoUser ProfileInfoUser { set; get; }
    }
}
