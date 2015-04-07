using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BeginMobile.Services.DTO
{
    public class GroupOptions
    {
        [JsonProperty("sections")]
        public List<string> GroupSections { set; get; }
    }
}
