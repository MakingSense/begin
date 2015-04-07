using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Interfaces;

namespace BeginMobile.Services.ManagerServices
{
    public class ContactManager
    {
        private const string BaseAddress = "http://186.109.86.251:5432/";
        private const string SubAddress = "begin/api/v1/";
        private const string Identifier = "contacts";


        private readonly GenericBaseClient<User> _contactClient =
            new GenericBaseClient<User>(BaseAddress, SubAddress);

        private readonly GenericBaseClient<ContactServiceError> _contactServiceClient =
            new GenericBaseClient<ContactServiceError>(BaseAddress, SubAddress);


        public List<User> GetContacts(
            string authToken,
            string name = null,
            string sort = null,
            string limit = null
            )
        {
            try
            {
                var urlGetParams = "?q=" + name + "&sort=" + sort + "&limit=" + limit;
                return _contactClient.GetListAsync(authToken, Identifier, urlGetParams).ToList();
            }
            catch (Exception exeption)
            {
                return null;
            }
        }

        public User GetContactById(string authToken, string contactId)
        {
            try
            {
                var urlId = "/" + contactId;
                return _contactClient.GetAsync(authToken, Identifier, urlId);
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public List<ContactServiceError> SendRequest(string authToken, string userName)
        {
            try
            {
                string addressSuffix = Identifier + "/send_request/" + userName;
                return _contactServiceClient.PostListAsync(authToken, null, addressSuffix).ToList();
            }
            catch (Exception exception)
            {
                var listError = new List<ContactServiceError>()
                {
                    new ContactServiceError
                    {
                        Message = exception.Message
                    }
                };
                return listError;
            }
        }

        public List<ContactServiceError> AcceptRequest(string authToken, string userName)
        {
            try
            {
                string addressSuffix = Identifier + "/accept_request/" + userName;
                return _contactServiceClient.PostListAsync(authToken, null, addressSuffix).ToList();
            }
            catch (Exception exception)
            {
                var listError = new List<ContactServiceError>()
                {
                    new ContactServiceError
                    {
                        Message = exception.Message
                    }
                };
                return listError;
            }
        }

        public List<ContactServiceError> RejectRequest(string authToken, string userName)
        {
            try
            {
                string addressSuffix = Identifier + "/reject_request/" + userName;
                return _contactServiceClient.PostListAsync(authToken, null, addressSuffix).ToList();
            }
            catch (Exception exception)
            {
                var listError = new List<ContactServiceError>()
                {
                    new ContactServiceError
                    {
                        Message = exception.Message
                    }
                };
                return listError;
            }
        }

        public List<ContactServiceError> RemoveFriendship(string authToken, string userName)
        {
            try
            {
                string addressSuffix = Identifier + "/remove/" + userName;
                return _contactServiceClient.PostListAsync(authToken, null, addressSuffix).ToList();
            }
            catch (Exception exception)
            {
                var listError = new List<ContactServiceError>()
                {
                    new ContactServiceError
                    {
                        Message = exception.Message
                    }
                };
                return listError;
            }
        }
    }
}
