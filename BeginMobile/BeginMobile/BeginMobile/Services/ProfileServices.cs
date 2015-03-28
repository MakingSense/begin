using BeginMobile.Services.DTO;
using BeginMobile.Services.ManagerServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeginMobile.Services
{
    public class ProfileServices
    {
        private ProfileManager _profileManger;

        public ProfileServices()
        {
            _profileManger = new ProfileManager();
        }

        public ProfileInformationGroups GetGroups(string userName, string authToken)
        {
            return _profileManger.GetGroupsInformation(userName, authToken);
        }

        public ProfileInfo GetInformation(string userName, string authToken)
        {
            return _profileManger.GetProfileInformation(userName, authToken);
        }

        public ProfileInformationActivities GetActivities(string userName, string authToken)
        {
            return _profileManger.GetActivitiesInformation(userName, authToken);
        }

        public ProfileInformationEvents GetEvents(string userName, string authToken)
        {
            return _profileManger.GetEventsInformation(userName, authToken);
        }

        public ProfileInformationContacts GetContacts(string userName, string authToken)
        {
            return _profileManger.GetContactsInformation(userName, authToken);
        }

        public ProfileInformationShop GetShopInfo(string userName, string authToken)
        {
            return _profileManger.GetShopInformation(userName, authToken);
        }

        public ProfileInformationMessages GetMessagesInfo(string userName, string authToken)
        {
            return _profileManger.GetMessagesInformation(userName, authToken);
        }

        public ProfileMeWall GetWall(string authToken, string filter = null, string type = null)
        {
            return _profileManger.GetWall(authToken, filter, type);
        }

        public ProfileInfo GetInformationDetail(string userName, string authToken)
        {
            return _profileManger.GetProfileInformationDetail(userName, authToken);
        }
    }
}
