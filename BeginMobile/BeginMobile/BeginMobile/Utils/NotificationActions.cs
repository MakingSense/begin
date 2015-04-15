using System.Collections.Generic;

namespace BeginMobile.Utils
{
    public enum NotificationOption
    {
        MarkAsRead = 0,
        MarkAsUnread = 1,
        Remove = 2
    }

    public enum NotificationComponent
    {
        Message = 0,
        Group = 1,
        Contact = 2,
        Activity = 3,
        Unknown = 4
    }
    public static class NotificationActions
    {
        public const string Read = "read";
        public const string Unread = "unread";

        private static readonly Dictionary<string, NotificationComponent> ComponentsDictionary =
            new Dictionary<string, NotificationComponent>
            {
                {"new_message", NotificationComponent.Message},
                {"group_invite", NotificationComponent.Group},
                {"friendship_request", NotificationComponent.Contact},
                {"friendship_accepted", NotificationComponent.Contact},
                {"friendship_rejected", NotificationComponent.Contact},
                {"friendship_removed", NotificationComponent.Contact},
                {"new_at_mention", NotificationComponent.Activity }
            };

        public async static void Request(NotificationOption notificationOption, string authToken,
            string notificationId)
        {
            switch (notificationOption)
            {
                case NotificationOption.MarkAsRead:
                    await App.ProfileServices.MarkAsRead(authToken, notificationId);
                    break;

                case NotificationOption.MarkAsUnread:
                    await App.ProfileServices.MarkAsUnread(authToken, notificationId);
                    break;
            }
        }
        public static NotificationComponent RetrieveComponent(string actionKey)
        {
            return ComponentsDictionary[actionKey];
        }

        public static string RetrieveFriendlyAction(string actionKey)
        {
            return actionKey.Replace("_", " ");
        }
    }
    public static class NotificationMessages
    {
        public const string DisplayAlert = "DisplayAlert";
        public const string MarkAsRead = "MarkAsRead";
        public const string MarkAsUnread = "MarkAsUnread";
        public const string RemoveNotification = "RemoveNotification";
    }
}


