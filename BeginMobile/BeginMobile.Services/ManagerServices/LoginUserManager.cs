using System;
using System.Collections.Generic;
using System.Net.Http;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Interfaces;
using System.Threading.Tasks;

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
            try
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("username", username),
                    new KeyValuePair<string, string>("password", password)
                });
                const string addressSuffix = "login";
                return await _loginManagerClient.PostAsync(content, addressSuffix);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<RegisterUser> Register(string username, string email, string password, string nameSurname)
        {
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
                return await _registerUserClient.PostAsync(content, addressSuffix);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string RetrievePassword(string email)
        {
            var content = new FormUrlEncodedContent(new[]
                                                    {
                                                        new KeyValuePair<string, string>("email", email),
                                                    });

            const string addressSuffix = "retrieve_password";
            return _stringResultClient.PostContentResultAsync(content, addressSuffix);
        }


        public async Task<ChangePassword> ChangeYourPassword(string currentPassword, string newPassword, string repeatNewPassword,
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
            return await _changePasswordClient.PostAsync(authToken, content, addressSuffix);
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
                return null;
            }
        }
    }
}