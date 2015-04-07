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
    }
}
