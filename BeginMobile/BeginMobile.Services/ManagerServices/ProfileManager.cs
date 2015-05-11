using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Interfaces;
using BeginMobile.Services.Utils;


namespace BeginMobile.Services.ManagerServices
{
    public class ProfileManager
    {
        private const string BaseAddress = "http://186.109.86.251:5432/";
        private const string SubAddress = "begin/api/v1/";
        private const string IdentifierProfile = "profile";
        private const string IdentifierMe = "me";

        private readonly GenericBaseClient<Wall> _wallClient;
        private readonly GenericBaseClient<ProfileInfo> _profileInfoClient;
        private readonly GenericBaseClient<ProfileInformationActivities> _profileActivityClient;
        private readonly GenericBaseClient<ProfileInformationEvents> _profileEventClient;
        private readonly GenericBaseClient<ProfileInformationGroups> _profileGroupClient;
        private readonly GenericBaseClient<ProfileContacts> _profileContactClient;
        private readonly GenericBaseClient<ProfileInformationShop> _profileShopClient;

        private readonly GenericBaseClient<ShopCategory> _shopCategoryClient =
    new GenericBaseClient<ShopCategory>(BaseAddress, SubAddress);


        private static readonly string ThisClassName = typeof(ProfileManager).Name;
        public ProfileManager()
        {
            _wallClient = new GenericBaseClient<Wall>(BaseAddress, SubAddress);
            _profileInfoClient = new GenericBaseClient<ProfileInfo>(BaseAddress, SubAddress);
            _profileEventClient = new GenericBaseClient<ProfileInformationEvents>(BaseAddress, SubAddress);
            _profileActivityClient = new GenericBaseClient<ProfileInformationActivities>(BaseAddress, SubAddress);
            _profileGroupClient =  new GenericBaseClient<ProfileInformationGroups>(BaseAddress, SubAddress);
            _profileContactClient = new GenericBaseClient<ProfileContacts>(BaseAddress, SubAddress);
            _profileShopClient = new GenericBaseClient<ProfileInformationShop>(BaseAddress, SubAddress);
        }

        public ProfileInfo GetProfileInformation(string username, string authToken)
        {
            try
            {
                var addressSuffix = IdentifierProfile + "/" + username;
                return _profileInfoClient.Get(authToken, addressSuffix, "");
            }
            catch (Exception exception)
            {
                AppContextError.Send(ThisClassName, "GetProfileInformation", exception, null, ExceptionLevel.Application);
                return null;
            }
        }

        public async Task<ProfileInfo> GetProfileInformationDetail(string authToken, string username)
        {
            try
            {
                const string urlGetParams = "?sections=details";
                var addressSuffix = IdentifierProfile + "/" + username;
                return await _profileInfoClient.GetAsync(authToken, addressSuffix, urlGetParams);
            }
            catch (Exception exception)
            {
                AppContextError.Send(ThisClassName, "GetProfileInformationDetail", exception, null, ExceptionLevel.Application);
                return null;
            }
        }

        public async Task<ProfileInformationActivities> GetActivitiesInformation(
            string authToken,
            string username = null,
            string limit = null,
            string offset = null)
        {
            try
            {
                var urlGetParams = "?sections=activities&limit=" + limit + "&offset=" + offset;
                
                if (username == null)
                {
                     return await _profileActivityClient.GetAsync(authToken, IdentifierMe, urlGetParams);
                }
                else
                {
                    var addressSuffix = IdentifierProfile + "/" + username;
                    return await _profileActivityClient.GetAsync(authToken, addressSuffix, urlGetParams);
                }
            }
            catch (Exception exception)
            {
                AppContextError.Send(ThisClassName, "GetActivitiesInformation", exception, null, ExceptionLevel.Application);
                return null;
            }
        }

        public ProfileInformationEvents GetEventsInformation(string username, string authToken)
        {
            try
            {
                const string urlGetParams = "?sections=events";
                var addressSuffix = IdentifierProfile + "/" + username;
                return _profileEventClient.Get(authToken, addressSuffix, urlGetParams);
            }
            catch (Exception exception)
            {
                AppContextError.Send(ThisClassName, "GetEventsInformation", exception, null, ExceptionLevel.Application);
                return null;
            }
        }

        public async Task<ProfileInformationGroups> GetGroupsInformation(string username, string authToken)
        {
            try
            {
                const string urlGetParams = "?sections=groups";
                var addressSuffix = IdentifierProfile + "/" + username;
                return await _profileGroupClient.GetAsync(authToken, IdentifierMe, urlGetParams);
            }
            catch (Exception exception)
            {
                AppContextError.Send(ThisClassName, "GetGroupsInformation", exception, null, ExceptionLevel.Application);
                return null;
            }
        }

        public async Task<ProfileContacts> GetContactsInformation(
            string authToken,
            string username = null,
            string limit = null,
            string offset = null
            )
        {
            try
            {
                var urlGetParams = "?sections=contacts&limit=" + limit + "&offset="+ offset;

                if (username == null)
                {
                    return await _profileContactClient.GetAsync(authToken, IdentifierMe, urlGetParams);
                }
                else
                {
                    var addressSuffix = IdentifierProfile + "/" + username;
                    return await _profileContactClient.GetAsync(authToken, addressSuffix, urlGetParams);
                }
            }
            catch (Exception exception)
            {
                AppContextError.Send(ThisClassName, "GetContactsInformation", exception, null, ExceptionLevel.Application);
                return null;
            }
        }

        public async Task<ProfileInformationShop> GetShopInformation(
            string authToken,
            string username = null,
            string limit = null,
            string offset = null
            )
        {
            try
            {
                var urlGetParams = "?sections=shop&limit=" + limit + "&offset=" + offset;

                if (username == null)
                {
                    return await _profileShopClient.GetAsync(authToken, IdentifierMe, urlGetParams);
                }
                else
                {
                    var addressSuffix = IdentifierProfile + "/" + username;
                    return await _profileShopClient.GetAsync(authToken, addressSuffix, urlGetParams);
                }
            }
            catch (Exception exception)
            {
                AppContextError.Send(ThisClassName, "GetShopInformation", exception, null, ExceptionLevel.Application);
                return null;
            }
        }

        public ProfileInformationMessages GetMessagesInformation(string username, string authToken)
        {
            //TODO For while this block code is static, change once that the api service is available
            try
            {
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
                profileMessages.GroupingMessage.MessagesGroup =
                    new ObservableCollection<IGrouping<string, Message>>(group);

                return profileMessages;
            }
            catch (Exception exception)
            {
                AppContextError.Send(ThisClassName, "GetMessagesInformation", exception, null, ExceptionLevel.Application);
                return null;
            }
        }

        public async Task<ProfileMeWall> GetWall(
            string authToken, 
            string filter = null, 
            string type = null, 
            string limit = null, 
            string offset = null
            )
        {
            try
            {
                var urlGetParams = "?filter=" + filter + "&type=" + type + "&limit=" + limit + "&offset=" + offset;
                const string addressSuffix = "me/wall";

                var profileMeWall = new ProfileMeWall()
                {
                    ListOfWall = await _wallClient.GetListAsync(authToken, addressSuffix, urlGetParams)
                };

                return profileMeWall;
            }
            catch (Exception exception)
            {
                AppContextError.Send(ThisClassName, "GetWall", exception, null, ExceptionLevel.Application);
                return null;
            }
        }

        public async Task<List<ShopCategory>> GetCategories(
            string authToken,
            string limit = null,
            string offset = null,
            string catId = null
            )
        {
            try
            {
                var urlGetParams = "?limit=" + limit + "&offset=" + offset + "&offset=" + catId;
                const string addressSuffix = "shop/categories";
                return await _shopCategoryClient.GetListAsync(authToken, addressSuffix, urlGetParams);

            }
            catch (Exception exception)
            {
                AppContextError.Send(ThisClassName, "GetCategories", exception, null, ExceptionLevel.Application);
                return null;
            }
        }

    }
}