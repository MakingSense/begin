﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace BeginMobile.Services.DTO
{
    public class ProfileInformationGroups
    {
        [JsonProperty("id")]
        public string Id { set; get; }

        [JsonProperty("username")]
        public string Username { set; get; }

        [JsonProperty("email")]
        public string Email { set; get; }

        [JsonProperty("url")]
        public string Url { set; get; }

        [JsonProperty("registered")]
        public string Registered { set; get; }

        [JsonProperty("name_surname")]
        public string NameSurname { set; get; }

        [JsonProperty("groups")]
        public List<Group> Groups { set; get; }
    }
}