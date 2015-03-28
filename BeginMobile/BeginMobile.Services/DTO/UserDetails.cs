using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BeginMobile.Services.DTO
{
    public class UserDetails
    {
        [JsonProperty("birthday")]
        public string Birthday { set; get; }

        [JsonProperty("gender")]
        public string Gender { set; get; }

        [JsonProperty("country")]
        public string Country { set; get; }

        [JsonProperty("state")]
        public string State { set; get; }

        [JsonProperty("city")]
        public string City { set; get; }

        [JsonProperty("address")]
        public string Address { set; get; }

        [JsonProperty("zipcode")]
        public string Zipcode { set; get; }

        [JsonProperty("phone")]
        public string Phone { set; get; }
    }
}
