using System.Collections.Generic;
using Newtonsoft.Json;

namespace BeginMobile.Services.DTO
{
    public class ProfileMeWall
    {
        public List<WallActivityItem> ListOfWall { set; get; }

        [JsonProperty("errors")]
        public ErrorRetrieved[] Errors { set; get; }
    }
}