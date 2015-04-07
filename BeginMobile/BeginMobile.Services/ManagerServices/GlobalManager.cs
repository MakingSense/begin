using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Interfaces;

namespace BeginMobile.Services.ManagerServices
{

    public class GlobalManager
    {
        private const string BaseAddress = "http://186.109.86.251:5432/";
        private const string SubAddress = "begin/api/v1/";

        private readonly GenericBaseClient<Options> _loginGlobalClient =
            new GenericBaseClient<Options>(BaseAddress, SubAddress);

        private readonly GenericBaseClient<GroupOptions> _loginGroupClient =
            new GenericBaseClient<GroupOptions>(BaseAddress, SubAddress);


        public Options GetMeOptions()
        {
            try
            {
                const string addressSuffix = "me/options";
                return _loginGlobalClient.GetAsync(addressSuffix);
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public GroupOptions GetGroupOptions()
        {
            try
            {
                const string addressSuffix = "groups/options";
                return _loginGroupClient.GetAsync(addressSuffix);
            }
            catch (Exception exception)
            {
                return null;
            }
        }
    }
}
