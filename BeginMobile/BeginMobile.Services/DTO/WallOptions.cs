using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BeginMobile.Services.DTO
{
    public class WallOptions
    {
        [JsonProperty("filter")]
        public List<string> Filter { set; get; }

        [JsonProperty("type")]
        public List<string> Type { set; get; }
    }
}
