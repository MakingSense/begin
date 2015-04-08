using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Interfaces;
using Newtonsoft.Json;

namespace BeginMobile.Services.ManagerServices
{
    public class ProfileManager
    {
        private const string BaseAddress = "http://186.109.86.251:5432/";
        private const string SubAddress = "begin/api/v1/";
        private const string Identifier = "profile";

        private readonly GenericBaseClient<Wall> _wallClient;
        private readonly GenericBaseClient<ProfileInfo> _profileInfoClient;
        private readonly GenericBaseClient<ProfileInformationActivities> _profileActivityClient;
        private readonly GenericBaseClient<ProfileInformationEvents> _profileEventClient;
        private readonly GenericBaseClient<ProfileInformationGroups> _profileGroupClient;
        private readonly GenericBaseClient<ProfileInformationContacts> _profileContactClient;
        private readonly GenericBaseClient<ProfileInformationShop> _profileShopClient;


        public ProfileManager()
        {
            _wallClient = new GenericBaseClient<Wall>(BaseAddress, SubAddress);
            _profileInfoClient = new GenericBaseClient<ProfileInfo>(BaseAddress, SubAddress);
            _profileEventClient = new GenericBaseClient<ProfileInformationEvents>(BaseAddress, SubAddress);
            _profileActivityClient = new GenericBaseClient<ProfileInformationActivities>(BaseAddress, SubAddress);
            _profileGroupClient =  new GenericBaseClient<ProfileInformationGroups>(BaseAddress, SubAddress);
            _profileContactClient = new GenericBaseClient<ProfileInformationContacts>(BaseAddress, SubAddress);
            _profileShopClient = new GenericBaseClient<ProfileInformationShop>(BaseAddress, SubAddress);
        }

        public ProfileInfo GetProfileInformation(string username, string authToken)
        {
            try
            {
                var addressSuffix = Identifier + "/" + username;
                return _profileInfoClient.Get(authToken, addressSuffix, "");
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public ProfileInfo GetProfileInformationDetail(string username, string authToken)
        {
            try
            {
                const string urlGetParams = "?sections=details";
                var addressSuffix = Identifier + "/" + username;
                return _profileInfoClient.Get(authToken, addressSuffix, urlGetParams);
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public ProfileInformationActivities GetActivitiesInformation(string username, string authToken)
        {
            try
            {
                const string urlGetParams = "?sections=activities";
                var addressSuffix = Identifier + "/" + username;
                return _profileActivityClient.Get(authToken, addressSuffix, urlGetParams);
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public ProfileInformationEvents GetEventsInformation(string username, string authToken)
        {
            try
            {
                const string urlGetParams = "?sections=events";
                var addressSuffix = Identifier + "/" + username;
                return _profileEventClient.Get(authToken, addressSuffix, urlGetParams);
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public async Task<ProfileInformationGroups> GetGroupsInformation(string username, string authToken)
        {
            try
            {
                const string urlGetParams = "?sections=groups";
                var addressSuffix = Identifier + "/" + username;
                return await _profileGroupClient.GetAsync(authToken, addressSuffix, urlGetParams);
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public ProfileInformationContacts GetContactsInformation(string username, string authToken)
        {
            try
            {
                const string urlGetParams = "?sections=contacts";
                var addressSuffix = Identifier + "/" + username;
                return _profileContactClient.Get(authToken, addressSuffix, urlGetParams);
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public ProfileInformationShop GetShopInformation(string username, string authToken)
        {
            try
            {
                const string urlGetParams = "?sections=shop";
                var addressSuffix = Identifier + "/" + username;
                return _profileShopClient.Get(authToken, addressSuffix, urlGetParams);
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public ProfileInformationMessages GetMessagesInformation(string username, string authToken)
        {
            //TODO For while this block code is static, change once that the api service is available
            var profileMessages = new ProfileInformationMessages
                                  {
                                      Messages = Message.Messages,
                                      GroupingMessage =
                                          new GroupingMessage
                                          {
                                              CountByGroup =
                                                  new Random().Next(0,
                                                      12)
                                          }
                                  };

            var group = profileMessages.Messages.GroupBy(x => x.Type).OrderBy(x => x.Key);
            profileMessages.GroupingMessage.MessagesGroup = new ObservableCollection<IGrouping<string, Message>>(group);

            return profileMessages;
        }

        public async Task<ProfileMeWall> GetWall(string authToken, string filter = null, string type = null)
        {
            try
            {
                var urlGetParams = "?filter=" + filter + "&type=" + type;
                const string addressSuffix = "me/wall";

                var profileMeWall = new ProfileMeWall()
                {
                    ListOfWall = await _wallClient.GetListAsync(authToken, addressSuffix, urlGetParams)
                };

                return profileMeWall;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}