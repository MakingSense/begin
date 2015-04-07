using System.Collections.Generic;
using BeginMobile.Services.DTO;
using BeginMobile.Services.ManagerServices;

namespace BeginMobile.Services
{
    public class ProfileServices
    {
        private readonly ProfileManager _profileManager;
        private readonly GroupManager _groupManager;
        private readonly EventManager _eventManager;
        private readonly ContactManager _contactManager;

        public ProfileServices()
        {
            _profileManager = new ProfileManager();
            _eventManager = new EventManager();
            _groupManager = new GroupManager();
            _contactManager = new ContactManager();
        }

        public ProfileInformationGroups GetGroups(string userName, string authToken)
        {
            return _profileManager.GetGroupsInformation(userName, authToken);
        }

        public ProfileInfo GetInformation(string userName, string authToken)
        {
            return _profileManager.GetProfileInformation(userName, authToken);
        }

        public ProfileInformationActivities GetActivities(string userName, string authToken)
        {
            return _profileManager.GetActivitiesInformation(userName, authToken);
        }

        public ProfileInformationEvents GetEvents(string userName, string authToken)
        {
            return _profileManager.GetEventsInformation(userName, authToken);
        }

        public ProfileInformationContacts GetContacts(string userName, string authToken)
        {
            return _profileManager.GetContactsInformation(userName, authToken);
        }

        public ProfileInformationShop GetShopInfo(string userName, string authToken)
        {
            return _profileManager.GetShopInformation(userName, authToken);
        }

        public ProfileInformationMessages GetMessagesInfo(string userName, string authToken)
        {
            return _profileManager.GetMessagesInformation(userName, authToken);
        }

        public ProfileMeWall GetWall(string authToken, string filter = null, string type = null)
        {
            return _profileManager.GetWall(authToken, filter, type);
        }

        public ProfileInfo GetInformationDetail(string userName, string authToken)
        {
            return _profileManager.GetProfileInformationDetail(userName, authToken);
        }

        //Groups, events and contacts
        public List<Group> GetGroupsByParams(string authToken, string name = null, string cat = null,
            string limit = null, string sections = null)
        {
            return _groupManager.GetGroupsByParams(authToken, name, cat, limit, sections);
        }

        public Group GetGroup(string authToken, string groupId, string sections = null)
        {
            return _groupManager.GetGroupById(authToken, groupId, sections);
        }

        public List<ProfileEvent> GetEventsByParams(string authToken, string name = null,
            string cat = null, string limit = null)
        {
            return _eventManager.GetEventsByParams(authToken, name, cat, limit);
        }

        public ProfileEvent GetEvent(string authToken, string eventId)
        {
            return _eventManager.GetEventById(authToken, eventId);
        }

        public List<User> GetContacts(string authToken, string name = null, string sort = null, string limit = null)
        {
            return _contactManager.GetContacts(authToken,name, sort, limit);
        }
    }
}