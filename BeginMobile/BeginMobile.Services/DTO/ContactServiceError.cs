using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BeginMobile.Services.DTO
{
    public class ContactServiceError
    {
        [JsonProperty("code")]
        public string ErrorCode { set; get; }

        [JsonProperty("label")]
        public string Message { set; get; }
    }
}
