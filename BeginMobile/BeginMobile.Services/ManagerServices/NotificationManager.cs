using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Interfaces;

namespace BeginMobile.Services.ManagerServices
{
    public class NotificationManager
    {
        private const string BaseAddress = "http://186.109.86.251:5432/";
        private const string SubAddress = "begin/api/v1/";
        private const string Identifier = "notifications";

        private readonly GenericBaseClient<ProfileNotification> _notificationClient =
            new GenericBaseClient<ProfileNotification>(BaseAddress, SubAddress);

        public async Task<ProfileNotification> GetProfileNotificationByParams(
            string authToken,
            string limit = null,
            string status = null
            )
        {
            try
            {
                var urlGetParams = "?limit=" + limit + "&status=" + status;
                return await _notificationClient.GetAsync(authToken, Identifier, urlGetParams);
            }
            catch (Exception exeption)
            {
                return null;
            }
        }

        public async Task<ProfileNotification> MarkAsRead(string authToken, string notificationId = null)
        {
            try
            {
                var notId = notificationId ?? "";
                string addressSuffix = Identifier + "/mark_as_read/" + notId;
                return await _notificationClient.PostAsync(authToken, null, addressSuffix);
            }
            catch (Exception exception)
            {
                var error = new ProfileNotification()
                {
                    Error = exception.Message
                };
                return error;
            }
        }

        public async Task<ProfileNotification> MarkAsUnread(string authToken, string notificationId = null)
        {
            try
            {
                var notId = notificationId ?? "";
                string addressSuffix = Identifier + "/mark_as_unread/" + notId;
                return await _notificationClient.PostAsync(authToken, null, addressSuffix);
            }
            catch (Exception exception)
            {
                var error = new ProfileNotification()
                {
                    Error = exception.Message
                };
                return error;
            }
        }
    }
}
