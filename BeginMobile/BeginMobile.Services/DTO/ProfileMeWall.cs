using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BeginMobile.Services.DTO
{
    public class ProfileMeWall
    {
        public List<Wall> ListOfWall { set; get; }

        [JsonProperty("errors")]
        public ErrorRetrieved[] Errors { set; get; }
    }
}
