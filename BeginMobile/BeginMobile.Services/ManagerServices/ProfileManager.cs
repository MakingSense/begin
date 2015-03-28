﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using BeginMobile.Services.DTO;
using Newtonsoft.Json;

namespace BeginMobile.Services.ManagerServices
{
    
    public class ProfileManager
    {
        private const string BaseAddress = "http://186.109.86.251:5432/";
        private const string SubAddress = "begin/api/v1/profile/";
        private const string SubAddressWall = "begin/api/v1/";

        public ProfileInfo GetProfileInformation(string username, string authToken)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("authtoken", authToken);
                ProfileInfo profileInfo = null;

                client.BaseAddress = new Uri(BaseAddress);

                var response = client.GetAsync(SubAddress + username).Result;

                if (response.IsSuccessStatusCode)
                {
                    var userJson = response.Content.ReadAsStringAsync().Result;
                    profileInfo = JsonConvert.DeserializeObject<ProfileInfo>(userJson);
                }
                return profileInfo;
            }
        }

        public ProfileInfo GetProfileInformationDetail(string username, string authToken)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("authtoken", authToken);
                ProfileInfo profileInfo = null;

                client.BaseAddress = new Uri(BaseAddress);

                var response = client.GetAsync(SubAddress + username + "?sections=details").Result;

                if (response.IsSuccessStatusCode)
                {
                    var userJson = response.Content.ReadAsStringAsync().Result;
                    profileInfo = JsonConvert.DeserializeObject<ProfileInfo>(userJson);
                }
                return profileInfo;
            }
        }

        public ProfileInformationActivities GetActivitiesInformation(string username, string authToken)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("authtoken", authToken);

                ProfileInformationActivities profileInformationActivities = null;

                client.BaseAddress = new Uri(BaseAddress);

                var response = client.GetAsync(SubAddress + username + "?sections=activities").Result;
                
                if (response.IsSuccessStatusCode)
                {
                    var userJson = response.Content.ReadAsStringAsync().Result;
                    profileInformationActivities = JsonConvert.DeserializeObject<ProfileInformationActivities>(userJson);
                }

                return profileInformationActivities;
            }
        }

        public ProfileInformationEvents GetEventsInformation(string username, string authToken)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("authtoken", authToken);

                ProfileInformationEvents profileInformationEvents = null;

                client.BaseAddress = new Uri(BaseAddress);

                var response = client.GetAsync(SubAddress + username + "?sections=events").Result;

                if (response.IsSuccessStatusCode)
                {
                    var userJson = response.Content.ReadAsStringAsync().Result;
                    profileInformationEvents = JsonConvert.DeserializeObject<ProfileInformationEvents>(userJson);
                }

                return profileInformationEvents;
            }
        }

        public ProfileInformationGroups GetGroupsInformation(string username, string authToken)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("authtoken", authToken);

                ProfileInformationGroups profileInformationGroups = null;

                client.BaseAddress = new Uri(BaseAddress);

                var response = client.GetAsync(SubAddress + username + "?sections=groups").Result;

                if (response.IsSuccessStatusCode)
                {
                    var userJson = response.Content.ReadAsStringAsync().Result;
                    profileInformationGroups = JsonConvert.DeserializeObject<ProfileInformationGroups>(userJson);
                }

                return profileInformationGroups;
            }
        }

        public ProfileInformationContacts GetContactsInformation(string username, string authToken)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("authtoken", authToken);

                ProfileInformationContacts profileInformationContacts = null;

                client.BaseAddress = new Uri(BaseAddress);

                var response = client.GetAsync(SubAddress + username + "?sections=contacts").Result;

                if (response.IsSuccessStatusCode)
                {
                    var userJson = response.Content.ReadAsStringAsync().Result;
                    profileInformationContacts = JsonConvert.DeserializeObject<ProfileInformationContacts>(userJson);
                }

                return profileInformationContacts;
            }
        }

        public ProfileInformationShop GetShopInformation(string username, string authToken)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("authtoken", authToken);

                ProfileInformationShop profileInformationShop = null;

                client.BaseAddress = new Uri(BaseAddress);

                var response = client.GetAsync(SubAddress + username + "?sections=shop").Result;

                if (response.IsSuccessStatusCode)
                {
                    var userJson = response.Content.ReadAsStringAsync().Result;
                    profileInformationShop = JsonConvert.DeserializeObject<ProfileInformationShop>(userJson);
                }

                return profileInformationShop;
            }
        }

        public ProfileInformationMessages GetMessagesInformation(string username, string authToken)
        {
            /*using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("authtoken", authToken);

                ProfileInformationShop profileInformationShop = null;

                client.BaseAddress = new Uri(BaseAddress);

                var response = client.GetAsync(SubAddress + username + "?sections=shop").Result;

                if (response.IsSuccessStatusCode)
                {
                    var userJson = response.Content.ReadAsStringAsync().Result;
                    profileInformationShop = JsonConvert.DeserializeObject<ProfileInformationShop>(userJson);
                }

                return profileInformationShop;
            }*/

            var profileMessages = new ProfileInformationMessages();
            profileMessages.Messages = Message.Messages;
            profileMessages.GroupingMessage = new GroupingMessage();
            profileMessages.GroupingMessage.CountByGroup = new Random().Next(0, 12);

            var group = profileMessages.Messages.GroupBy(x =>x.Type).OrderBy(x => x.Key);
            profileMessages.GroupingMessage.MessagesGroup = new ObservableCollection<IGrouping<string, Message>>(group);

            return profileMessages;
        }

        public ProfileMeWall GetWall(string authToken, string filter = null, string type = null)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("authtoken", authToken);

                client.BaseAddress = new Uri(BaseAddress);

                var urlGetParams = "";

                if (!(string.IsNullOrEmpty(filter) && string.IsNullOrEmpty(type)))
                {
                    //
                    urlGetParams = "?filter=" + filter + "&type=" + type;
                }
                else if (!string.IsNullOrEmpty(filter))
                {
                    //
                    urlGetParams = "?filter=" + filter;
                }
                else if (!string.IsNullOrEmpty(type))
                {
                    //
                    urlGetParams = "?type=" + type;
                }


                var response = client.GetAsync(SubAddressWall + "me/wall" + urlGetParams).Result;
                var userJson = response.Content.ReadAsStringAsync().Result;

                var profileMeWall = new ProfileMeWall();

                if (response.IsSuccessStatusCode)
                {
                    var listWall = JsonConvert.DeserializeObject<List<Wall>>(userJson);
                    profileMeWall.ListOfWall = listWall;
                }
                else
                {
                    profileMeWall = JsonConvert.DeserializeObject<ProfileMeWall>(userJson);
                }

                return profileMeWall;
            }
        }
       
    }

}