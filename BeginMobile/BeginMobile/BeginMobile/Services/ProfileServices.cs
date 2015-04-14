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
        private readonly NotificationManager _notificationManager;
        private readonly MessageManager _messageManager;

        public ProfileServices()
        {
            _profileManager = new ProfileManager();
            _eventManager = new EventManager();
            _groupManager = new GroupManager();
            _contactManager = new ContactManager();
            _notificationManager = new NotificationManager();
            _messageManager = new MessageManager();
        }

        public async Task<ProfileInformationGroups> GetGroups(string userName, string authToken)
        {
            return await _profileManager.GetGroupsInformation(userName, authToken);
        }

        public ProfileInfo GetInformation(string userName, string authToken)
        {
            return _profileManager.GetProfileInformation(userName, authToken);
        }

        public async Task<ProfileInformationActivities> GetActivities(string userName, string authToken)
        {
            return await _profileManager.GetActivitiesInformation(userName, authToken);
        }

        public ProfileInformationEvents GetEvents(string userName, string authToken)
        {
            return _profileManager.GetEventsInformation(userName, authToken);
        }

        public ProfileInformationContacts GetContacts(string userName, string authToken)
        {
            return _profileManager.GetContactsInformation(userName, authToken);
        }

        public async Task<ProfileInformationShop> GetShopInfo(string userName, string authToken)
        {
            return await _profileManager.GetShopInformation(userName, authToken);
        }

        public ProfileInformationMessages GetMessagesInfo(string userName, string authToken)
        {
            return _profileManager.GetMessagesInformation(userName, authToken);
        }

        public async Task<ProfileMeWall> GetWall(string authToken, string filter = null, string type = null)
        {
            return await _profileManager.GetWall(authToken, filter, type);
        }

        public async Task<ProfileInfo> GetInformationDetail(string userName, string authToken)
        {
            return await _profileManager.GetProfileInformationDetail(userName, authToken);
        }

        //Groups, events and contacts
        public async Task<List<Group>> GetGroupsByParams(string authToken, string name = null, string cat = null,
            string limit = null, string sections = null)
        {
            return await _groupManager.GetGroupsByParams(authToken, name, cat, limit, sections);
        }

        public async Task<Group> GetGroup(string authToken, string groupId, string sections = null)
        {
            return await _groupManager.GetGroupById(authToken, groupId, sections);
        }

        public async Task<List<ProfileEvent>> GetEventsByParams(string authToken, string name = null,
            string cat = null, string limit = null)
        {
            return await _eventManager.GetEventsByParams(authToken, name, cat, limit);
        }

        public ProfileEvent GetEvent(string authToken, string eventId)
        {
            return _eventManager.GetEventById(authToken, eventId);
        }

        public async Task<List<User>> GetContacts(string authToken, string name = null, string sort = null, string limit = null)
        {
            return await _contactManager.GetContacts(authToken,name, sort, limit);
        }

        public User GetContact(string authToken, string contactId)
        {
            return _contactManager.GetContactById(authToken, contactId);
        }

        //Contact options 
        public List<ServiceError> SendRequest(string authToken, string userName)
        {
            return _contactManager.SendRequest(authToken, userName);
        }

        public List<ServiceError> AcceptRequest(string authToken, string userName)
        {
            return _contactManager.AcceptRequest(authToken, userName);
        }

        public List<ServiceError> RejectRequest(string authToken, string userName)
        {
            return _contactManager.RejectRequest(authToken, userName);
        }

        public List<ServiceError> RemoveFriendship(string authToken, string userName)
        {
            return _contactManager.RemoveFriendship(authToken, userName);
        }

        //Notifications
        public async Task<ProfileNotification> GetProfileNotification(string authToken, string limit = null, string status = null)
        {
            return await _notificationManager.GetProfileNotificationByParams(authToken, limit, status);
        }

        public async Task<ProfileNotification> MarkAsRead(string authToken, string notificationId = null)
        {
            return await _notificationManager.MarkAsRead(authToken, notificationId);
        }

        public async Task<ProfileNotification> MarkAsUnread(string authToken, string notificationId = null)
        {
            return await _notificationManager.MarkAsUnread(authToken, notificationId);
        }

        //Messages
        public async Task<ProfileThreadMessages> GetProfileThreadMessagesInbox(string authToken)
        {
            return await _messageManager.GetProfileThreadMessagesInbox(authToken);
        }

        public async Task<ProfileThreadMessages> GetProfileThreadMessagesSent(string authToken)
        {
            return await _messageManager.GetProfileThreadMessagesSent(authToken);
        }

        public async Task<ProfileThreadMessages> SendMessage(string authToken, string to, string subject, string message, string threadId = null)
        {
            return await _messageManager.SendMessage(authToken, to, subject, message, threadId);
        }

        public async Task<List<Message>> GetMessagesByThread(string authToken, string threadId)
        {
            return await _messageManager.GetThreadMessages(authToken, threadId);
        }

        public async Task<ProfileThreadMessages> MarkAsReadByThread(string authToken, string threadId)
        {
            return await _messageManager.MarkAsReadThreadMessages(authToken, threadId);
        }

        public async Task<ProfileThreadMessages> MarkAsUnreadByThread(string authToken, string threadId)
        {
            return await _messageManager.MarkAsUnreadThreadMessages(authToken, threadId);
        }

        public async Task<ProfileThreadMessages> DeleteByThread(string authToken, string threadId)
        {
            return await _messageManager.DeleteThreadMessages(authToken, threadId);
        }
    }
}