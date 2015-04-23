using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Interfaces;
using BeginMobile.Services.Utils;

namespace BeginMobile.Services.ManagerServices
{
    public class ContactManager
    {
        private const string BaseAddress = "http://186.109.86.251:5432/";
        private const string SubAddress = "begin/api/v1/";
        private const string Identifier = "contacts";
        private const string IdentifierAux = "users";


        private readonly GenericBaseClient<User> _contactClient =
            new GenericBaseClient<User>(BaseAddress, SubAddress);

        private readonly GenericBaseClient<ServiceError> _contactServiceClient =
            new GenericBaseClient<ServiceError>(BaseAddress, SubAddress);

        private readonly GenericBaseClient<ProfileContacts> _profileContactsClient =
            new GenericBaseClient<ProfileContacts>(BaseAddress, SubAddress);


        public async  Task<List<User>> GetContacts(
            string authToken,
            string name = null,
            string sort = null,
            string limit = null,
            string offset = null
            )
        {
            try
            {
                var urlGetParams = "?q=" + name + "&sort=" + sort + "&limit=" + limit + "&offset=" + offset;
                return await _contactClient.GetListAsync(authToken, IdentifierAux, urlGetParams);
            }
            catch (Exception exception)
            {
                AppContextError.Send(exception, null, ExceptionLevel.Application);
                return null;
            }
        }

        public User GetContactById(string authToken, string contactId)
        {
            try
            {
                var urlId = "/" + contactId;
                return _contactClient.Get(authToken, Identifier, urlId);
            }
            catch (Exception exception)
            {
                AppContextError.Send(exception, null, ExceptionLevel.Application);
                return null;
            }
        }

        public List<ServiceError> SendRequest(string authToken, string userName)
        {
            try
            {
                string addressSuffix = Identifier + "/send_request/" + userName;
                return _contactServiceClient.PostList(authToken, null, addressSuffix).ToList();
            }
            catch (Exception exception)
            {
                var listError = new List<ServiceError>()
                {
                    new ServiceError
                    {
                        ErrorMessage = exception.Message
                    }
                };

                AppContextError.Send(exception, null, ExceptionLevel.Application);
                return listError;
            }
        }

        public List<ServiceError> AcceptRequest(string authToken, string userName)
        {
            try
            {
                string addressSuffix = Identifier + "/accept_request/" + userName;
                return _contactServiceClient.PostList(authToken, null, addressSuffix).ToList();
            }
            catch (Exception exception)
            {
                var listError = new List<ServiceError>()
                {
                    new ServiceError
                    {
                        ErrorMessage = exception.Message
                    }
                };

                AppContextError.Send(exception, null, ExceptionLevel.Application);
                return listError;
            }
        }

        public List<ServiceError> RejectRequest(string authToken, string userName)
        {
            try
            {
                string addressSuffix = Identifier + "/reject_request/" + userName;
                return _contactServiceClient.PostList(authToken, null, addressSuffix).ToList();
            }
            catch (Exception exception)
            {
                var listError = new List<ServiceError>()
                {
                    new ServiceError
                    {
                        ErrorMessage = exception.Message
                    }
                };

                AppContextError.Send(exception, null, ExceptionLevel.Application);
                return listError;
            }
        }

        public List<ServiceError> RemoveFriendship(string authToken, string userName)
        {
            try
            {
                string addressSuffix = Identifier + "/remove/" + userName;
                return _contactServiceClient.PostList(authToken, null, addressSuffix).ToList();
            }
            catch (Exception exception)
            {
                var listError = new List<ServiceError>()
                {
                    new ServiceError
                    {
                        ErrorMessage = exception.Message
                    }
                };

                AppContextError.Send(exception, null, ExceptionLevel.Application);
                return listError;
            }
        }

        public async Task<ProfileContacts> CancelRequest(string authToken, string userName)
        {
            try
            {
                ProfileContacts profileContacts = null;
                string addressSuffix = Identifier + "/cancel_request/" + userName;

                var listResult = await _contactServiceClient.PostListAsync(authToken, null, addressSuffix);

                if (listResult != null)
                {
                    profileContacts = new ProfileContacts()
                    {
                        Errors = listResult
                    };
                }

                return profileContacts;
            } 
            catch (Exception exception)
            {
                var error = new ProfileContacts()
                {
                    Error = exception.Message,
                };

                AppContextError.Send(exception, error, ExceptionLevel.Application);
                return error;
            }
        }

        //TODO: (Temporal) move to UserManager
        public User GetUserById(string authToken, string userId)
        {
            try
            {
               return 
                    _contactClient.GetList(authToken, IdentifierAux, string.Empty)
                        .FirstOrDefault(u => u.Id == int.Parse(userId));
            }

            catch (Exception exception)
            {
                AppContextError.Send(exception, ExceptionLevel.Application);
                return null;
            }
        }
    }
}
