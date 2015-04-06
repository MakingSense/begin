using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BeginMobile.Services.DTO
{
    public class Group
    {
        [JsonProperty("group_id")]
        public string Id { set; get; }

        [JsonProperty("creator_id")]
        public string CreatorId { set; get; }

        [JsonProperty("name")]
        public string Name { set; get; }

        [JsonProperty("description")]
        public string Description { set; get; }

        [JsonProperty("status")]
        public string Status { set; get; }

        [JsonProperty("date_created")]
        public string DateCreated { set; get; }

        [JsonProperty("is_admin")]
        public string IsAdmin { set; get; }

        [JsonProperty("creator")]
        public User Creator { set; get; }

        [JsonProperty("slug")]
        public string Slug { set; get; }

        [JsonProperty("owner")]
        public User Owner { set; get; }

        public string Category { set; get; }

        public string Type { set; get; }

        public string MembersContent { set; get; }

        [JsonProperty("members")]
        public List<User> Members { set; get; }

        //TODO remove this region when it's not necessary

        #region "Fields for test"

        private static List<Group> _listGroup;

        public static List<Group> ListGroup
        {
            get
            {
                if (_listGroup == null)
                {
                    _listGroup = new List<Group>();
                    for (var i = 0; i < 15; i++)
                    {
                        var group = new Group()
                                    {
                                        Id = i.ToString(),
                                        Name = "Name " + i,
                                        Type = "Type " + i,
                                        Category = "Category " + i,
                                        MembersContent = ((new Random().Next(1, 100)) + i).ToString(),
                                    };
                        _listGroup.Add(group);
                    }
                }

                return _listGroup;
            }
        }

        #endregion
    }
}