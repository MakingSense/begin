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
            return _profileManger.GetProfileInformation(userName, userName);
        }

        public ProfileInformationActivities GetActivities(string userName, string authToken)
        {
            return _profileManger.GetActivitiesInformation(userName, userName);
        }

        public ProfileInformationEvents GetEvents(string userName, string authToken)
        {
            return _profileManger.GetEventsInformation(userName, userName);
        }

        public ProfileInformationContacts GetContacts(string userName, string authToken)
        {
            return _profileManger.GetContactsInformation(userName, userName);
        }

    }
}
