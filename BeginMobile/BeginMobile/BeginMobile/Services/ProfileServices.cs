using System.Collections.Generic;
using BeginMobile.Services.DTO;
using BeginMobile.Services.ManagerServices;
using System.Threading.Tasks;

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

        public async Task<ProfileInformationGroups> GetGroups(string userName, string authToken)
        {
            return await _profileManager.GetGroupsInformation(userName, authToken);
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

        public async Task<ProfileMeWall> GetWall(string authToken, string filter = null, string type = null)
        {
            return await _profileManager.GetWall(authToken, filter, type);
        }

        public ProfileInfo GetInformationDetail(string userName, string authToken)
        {
            return _profileManager.GetProfileInformationDetail(userName, authToken);
        }

        //Groups, events and contacts
        public async Task<List<Group>> GetGroupsByParams(string authToken, string name = null, string cat = null,
            string limit = null, string sections = null)
        {
            return await _groupManager.GetGroupsByParams(authToken, name, cat, limit, sections);
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

        public User GetContact(string authToken, string contactId)
        {
            return _contactManager.GetContactById(authToken, contactId);
        }

        //Contact options 
        public List<ContactServiceError> SendRequest(string authToken, string userName)
        {
            return _contactManager.SendRequest(authToken, userName);
        }

        public List<ContactServiceError> AcceptRequest(string authToken, string userName)
        {
            return _contactManager.AcceptRequest(authToken, userName);
        }

        public List<ContactServiceError> RejectRequest(string authToken, string userName)
        {
            return _contactManager.RejectRequest(authToken, userName);
        }

        public List<ContactServiceError> RemoveFriendship(string authToken, string userName)
        {
            return _contactManager.RemoveFriendship(authToken, userName);
        }
    }
}