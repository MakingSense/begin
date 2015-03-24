using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeginMobile.Services.DTO
{
    public class ChangePassword
    {
        [JsonProperty("errors")]
        public ErrorRetrieved[] Errors { set; get; }
    }
}
