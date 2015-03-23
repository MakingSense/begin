using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeginMobile.Services.DTO
{
    public class ErrorRetrieved
    {
        [JsonProperty("code")]
        public string Code { set; get; }

        [JsonProperty("label")]
        public string Label{ set; get; }
    }
}
