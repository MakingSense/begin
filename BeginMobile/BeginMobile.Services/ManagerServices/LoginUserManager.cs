using System;
using System.Collections.Generic;
using System.Net.Http;
using BeginMobile.Services.DTO;
using Newtonsoft.Json;

namespace BeginMobile.Services.ManagerServices
{
    public class LoginUserManager
    {
        private const string BaseAddress = "http://186.109.86.251:5432/";

        private const string SubAddress = "begin/api/v1/";

        public LoginUser Login(string username, string password)
        {
            using (var client = new HttpClient())
            {
                LoginUser resultLoginUser = null;

                client.BaseAddress = new Uri(BaseAddress);

                var content = new FormUrlEncodedContent(new[]
                                                        {
                                                            new KeyValuePair<string, string>("username", username),
                                                            new KeyValuePair<string, string>("password", password)
                                                        });

                var response = client.PostAsync(SubAddress + "login", content).Result;
                var userJson = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    resultLoginUser = JsonConvert.DeserializeObject<LoginUser>(userJson);
                }

                return resultLoginUser;
            }
        }

        public RegisterUser Register(string username, string email, string password, string nameSurname)
        {
            using (var client = new HttpClient())
            {
                RegisterUser resultRegisterUser = null;

                client.BaseAddress = new Uri(BaseAddress);

                var content = new FormUrlEncodedContent(new[]
                                                        {
                                                            new KeyValuePair<string, string>("username", username),
                                                            new KeyValuePair<string, string>("email", email),
                                                            new KeyValuePair<string, string>("password", password),
                                                            new KeyValuePair<string, string>("name_surname", nameSurname)
                                                        });

                var response = client.PostAsync(SubAddress + "signup", content).Result;
                var userJson = response.Content.ReadAsStringAsync().Result;

                resultRegisterUser = JsonConvert.DeserializeObject<RegisterUser>(userJson);

                return resultRegisterUser;
            }
        }

        public string RetrievePassword(string email)
        {
            var result = "";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAddress);

                var content = new FormUrlEncodedContent(new[]
                                                        {
                                                            new KeyValuePair<string, string>("email", email),
                                                        });

                var response = client.PostAsync(SubAddress + "retrieve_password", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var userJson = response.Content.ReadAsStringAsync().Result;
                    result = userJson;
                }

                return result;
            }
        }


        public ChangePassword ChangeYourPassword(string currentPassword, string newPassword, string repeatNewPassword, string authToken)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("authtoken", authToken);
                client.BaseAddress = new Uri(BaseAddress);

                var content = new FormUrlEncodedContent(new[]
                                                        {
                                                            new KeyValuePair<string, string>("current_password", currentPassword),
                                                            new KeyValuePair<string, string>("new_password", newPassword),
                                                            new KeyValuePair<string, string>("repeat_new_password", repeatNewPassword)
                                                        });

                var response = client.PostAsync(SubAddress + "me/change_password", content).Result;
                var retrievedJsonData = response.Content.ReadAsStringAsync().Result;


                return JsonConvert.DeserializeObject<ChangePassword>(retrievedJsonData);

            }
        }
    }
}