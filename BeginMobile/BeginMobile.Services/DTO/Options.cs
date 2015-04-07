using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BeginMobile.Services.DTO
{
    public class Options
    {
        [JsonProperty("sections")]
        public List<string> Sections { set; get; }

        [JsonProperty("wall")]
        public WallOptions WallOptions { set; get; }
    }
}
