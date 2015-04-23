using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Interfaces;
using System.Threading.Tasks;
using BeginMobile.Services.Utils;
using Xamarin.Forms;

namespace BeginMobile.Services.ManagerServices
{
    public class LoginUserManager
    {
        private const string BaseAddress = "http://186.109.86.251:5432/";
        private const string SubAddress = "begin/api/v1/";

        private readonly GenericBaseClient<LoginUser> _loginManagerClient =
            new GenericBaseClient<LoginUser>(BaseAddress, SubAddress);

        private readonly GenericBaseClient<RegisterUser> _registerUserClient =
            new GenericBaseClient<RegisterUser>(BaseAddress, SubAddress);

        private readonly GenericBaseClient<ChangePassword> _changePasswordClient =
            new GenericBaseClient<ChangePassword>(BaseAddress, SubAddress);

        private readonly GenericBaseClient<string> _stringResultClient =
            new GenericBaseClient<string>(BaseAddress, SubAddress);

        public async Task<LoginUser> Login(string username, string password)
        {
            var loginUser = new LoginUser();

            try
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("username", username),
                    new KeyValuePair<string, string>("password", password)
                });

                const string addressSuffix = "login";
                loginUser = await _loginManagerClient.PostAsync(content, addressSuffix);
                return loginUser;
            }

            catch (Exception ex)
            {
                AppContextError.Send(ex, loginUser, ExceptionLevel.Application);
                return null;
            }
        }

        private const int RegisterTimeout = 8500; //8.5 secs

        public async Task<RegisterUser> Register(string username, string email, string password, string nameSurname)
        {
            var registeredUser = new RegisterUser();

            try
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("username", username),
                    new KeyValuePair<string, string>("email", email),
                    new KeyValuePair<string, string>("password", password),
                    new KeyValuePair<string, string>("name_surname", nameSurname)
                });

                const string addressSuffix = "signup";
                registeredUser = await _registerUserClient.PostAsync(content, addressSuffix, RegisterTimeout);
                return registeredUser;
            }
            catch (Exception ex)
            {
                AppContextError.Send(ex, registeredUser, ExceptionLevel.Application);
                return null;
            }
        }

        public async Task<string> RetrievePassword(string email)
        {
            try
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("email", email),
                });

                const string addressSuffix = "retrieve_password";
                return await _stringResultClient.PostContentResultAsync(content, addressSuffix);
            }
            catch (Exception ex)
            {
                AppContextError.Send(ex, null, ExceptionLevel.Application);
                return null;
            }
        }

        public async Task<ChangePassword> ChangeYourPassword(string currentPassword, string newPassword,
            string repeatNewPassword,
            string authToken)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("current_password",
                    currentPassword),
                new KeyValuePair<string, string>("new_password", newPassword),
                new KeyValuePair<string, string>("repeat_new_password",
                    repeatNewPassword)
            });

            const string addressSuffix = "me/change_password";

            try
            {
                return await _changePasswordClient.PostAsync(authToken, content, addressSuffix);
            }

            catch (Exception exception)
            {
                AppContextError.Send(exception, ExceptionLevel.Application);
                return null;
            }
        }

        public async Task<string> UpdateProfile(string nameSurname, string authToken)
        {
            try
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("name_surname", nameSurname),
                });

                const string addressSuffix = "me/update_profile";
                return await _stringResultClient.PostContentResultAsync(authToken, content, addressSuffix);
            }

            catch (Exception exception)
            {
                AppContextError.Send(exception, ExceptionLevel.Application);
                return null;
            }
        }
    }
}