
using System.Collections.Generic;
using System.Threading.Tasks;
using BeginMobile.Services.DTO;

namespace BeginMobile.Utils
{
    public enum NotificationOption
    {
        MarkAsRead = 0,
        MarkAsUnread = 1,
        Remove = 2
    }
    public static class NotificationActions
    {
        public const string Read = "read";
        public const string Unread = "unread";

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
    }
    public static class NotificationMessages
    {
        public const string DisplayAlert = "DisplayAlert";
        public const string MarkAsRead = "MarkAsRead";
        public const string MarkAsUnread = "MarkAsUnread";
        public const string RemoveNotification = "RemoveNotification";
    }
}


