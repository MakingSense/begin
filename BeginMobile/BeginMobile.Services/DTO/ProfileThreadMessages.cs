using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BeginMobile.Services.DTO
{
    public class ProfileThreadMessages:BaseServiceError
    {
        [JsonProperty("threads")]
        public List<ThreadMessages> Threads { set; get; }

        [JsonProperty("thread_count")]
        public string ThreadCount { set; get; }
    }
}
