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

    public class UserEducation
    {
        [JsonProperty("study_level")]
        public string StudyLevel { set; get; }

        [JsonProperty("establishment")]
        public string Establishment { set; get; }

        [JsonProperty("title")]
        public string Title { set; get; }

        [JsonProperty("profession")]
        public string Profession { set; get; }

        [JsonProperty("other_activity")]
        public string OtherActivity { set; get; }
    }

    public class Userwork
    {
        [JsonProperty("company_name")]
        public string CompanyName { set; get; }

        [JsonProperty("position")]
        public string Position { set; get; }

        [JsonProperty("city")]
        public string City { set; get; }

        [JsonProperty("start")]
        public string Start { set; get; }
    }
}