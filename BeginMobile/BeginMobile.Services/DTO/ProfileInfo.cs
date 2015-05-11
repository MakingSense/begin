using Newtonsoft.Json;

namespace BeginMobile.Services.DTO
{
    public class ProfileInfo
    {
        [JsonProperty("id")]
        public int Id { set; get; }

        [JsonProperty("username")]
        public string UserName { set; get; }

        [JsonProperty("nicename")]
        public string NiceName { set; get; }

        [JsonProperty("name_surname")]
        public string NameSurname { set; get; }

        [JsonProperty("email")]
        public string Email { set; get; }

        [JsonProperty("url")]
        public string Url { set; get; }

        [JsonProperty("registered")]
        public string Registered { set; get; }

        [JsonProperty("displayname")]
        public string DisplayName { set; get; }

        [JsonProperty("firstname")]
        public string FirstName { set; get; }

        [JsonProperty("lastname")]
        public string Lastname { set; get; }

        [JsonProperty("nickname")]
        public string NickName { set; get; }

        [JsonProperty("description")]
        public string Description { set; get; }

        [JsonProperty("bithday")]
        public string BirthDay { set; get; }

        [JsonProperty("gender")]
        public char Gender { set; get; }

        [JsonProperty("address")]
        public string Address { set; get; }

        [JsonProperty("country")]
        public string Country { set; get; }

        [JsonProperty("state")]
        public string State { set; get; }

        [JsonProperty("city")]
        public string City { set; get; }

        [JsonProperty("phone")]
        public int Phone { set; get; }

        [JsonProperty("cellphone")]
        public int CellPhone { set; get; }

        [JsonProperty("skype")]
        public string Skype { set; get; }

        [JsonProperty("others")]
        public string Others { set; get; }

        [JsonProperty("educationlevel")]
        public string EducationLevel { set; get; }

        [JsonProperty("establishment")]
        public string Establishment { set; get; }

        [JsonProperty("educationtitle")]
        public string EducationTitle { set; get; }

        [JsonProperty("educationcategory")]
        public string EducationCategory { set; get; }

        [JsonProperty("educationsubcategory")]
        public string EducationSubcategory { set; get; }

        [JsonProperty("company")]
        public string Company { set; get; }

        [JsonProperty("position")]
        public string Position { set; get; }

        [JsonProperty("citywork")]
        public string CityWork { set; get; }

        [JsonProperty("workdescription")]
        public string WorkDescription { set; get; }

        [JsonProperty("currentwork")]
        public string CurrentWork { set; get; }

        [JsonProperty("dates")]
        public string Dates { set; get; }

        [JsonProperty("details")]
        public UserDetails Details { set; get; }

        [JsonProperty("education")]
        public UserEducation Education { set; get; }

        [JsonProperty("work")]
        public Userwork Work { set; get; }
    }
}