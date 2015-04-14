using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Interfaces;

namespace BeginMobile.Services.ManagerServices
{
    public class MessageManager
    {
        private const string BaseAddress = "http://186.109.86.251:5432/";
        private const string SubAddress = "begin/api/v1/";
        private const string Identifier = "messages";

        private readonly GenericBaseClient<ProfileThreadMessages> _profileThreadMessagesClient =
            new GenericBaseClient<ProfileThreadMessages>(BaseAddress, SubAddress);


        private readonly GenericBaseClient<BaseServiceError> _updateThreadMessagesClient =
            new GenericBaseClient<BaseServiceError>(BaseAddress, SubAddress);

        public async Task<ProfileThreadMessages> GetProfileThreadMessagesInbox(string authToken)
        {
            try
            {
                const string addressSuffix = Identifier + "/inbox";
                return await _profileThreadMessagesClient.GetAsync(authToken, addressSuffix);
            }
            catch (Exception exception)
            {
                var profileThreadMessages = new ProfileThreadMessages()
                {
                    Error = exception.Message
                };

                return profileThreadMessages;
            }
        }

        public async Task<ProfileThreadMessages> GetProfileThreadMessagesSent(string authToken)
        {
            try
            {
                const string addressSuffix = Identifier + "/sentbox";
                return await _profileThreadMessagesClient.GetAsync(authToken, addressSuffix);
            }
            catch (Exception exception)
            {
                var profileThreadMessages = new ProfileThreadMessages()
                {
                    Error = exception.Message
                };

                return profileThreadMessages;
            }
        }

        public async Task<BaseServiceError> SendMessage(string authToken, string to, string subject, string message, string threadId = null)
        {
            try
            {
                var contentValues = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("to", to),
                    new KeyValuePair<string, string>("subject", subject),
                    new KeyValuePair<string, string>("message", message)
                };

                if (!string.IsNullOrEmpty(threadId))
                {
                    contentValues.Add(new KeyValuePair<string, string>("thread_id", threadId));
                }

                var content = new FormUrlEncodedContent(contentValues.ToArray());

                const string addressSuffix = Identifier + "/send";
                return await _updateThreadMessagesClient.PostAsync(authToken, content, addressSuffix);
            }
            catch (Exception exception)
            {
                var error = new BaseServiceError()
                {
                    Error = exception.Message,
                };
                return error;
            }
        }


    }
}
