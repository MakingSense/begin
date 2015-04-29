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

    public class GlobalManager
    {
        private const string BaseAddress = "http://186.109.86.251:5432/";
        private const string SubAddress = "begin/api/v1/";

        private readonly GenericBaseClient<GlobalOptions> _loginGlobalClient =
            new GenericBaseClient<GlobalOptions>(BaseAddress, SubAddress);

        private readonly GenericBaseClient<GroupOptions> _loginGroupClient =
            new GenericBaseClient<GroupOptions>(BaseAddress, SubAddress);
        private static readonly string ThisClassName = typeof(GroupManager).Name;

        public async Task<GlobalOptions> GetMeOptions()
        {
            try
            {
                const string addressSuffix = "me/options";
                return await _loginGlobalClient.GetAsync(addressSuffix);
            }
            catch (Exception exception)
            {
                AppContextError.Send(ThisClassName, "GetMeOptions", exception, null, ExceptionLevel.Application);
                return null;
            }
        }

        public async Task<GroupOptions> GetGroupOptions()
        {
            try
            {
                const string addressSuffix = "groups/options";
                return await _loginGroupClient.GetAsync(addressSuffix);
            }
            catch (Exception exception)
            {
                AppContextError.Send(ThisClassName, "GetGroupOptions", exception, null, ExceptionLevel.Application);
                return null;
            }
        }

    }
}
