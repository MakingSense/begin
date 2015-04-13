using System;
using System.Collections.Generic;
using System.Linq;
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

    }
}
