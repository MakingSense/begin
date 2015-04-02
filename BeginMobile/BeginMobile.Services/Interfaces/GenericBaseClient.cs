using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Threading.Tasks;
using System.Collections;


namespace BeginMobile.Services.Interfaces
{
    public class GenericBaseClient<T> where T : class
    {
        private readonly string _serviceBaseAddress;
        private readonly string _subAddress;

        public GenericBaseClient(string baseAddress, string subAddress)
        {
            _serviceBaseAddress = baseAddress;
            _subAddress = subAddress;
        }

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="identifier">The identifier examples user, groups, etc</param>
        /// <param name="urlParams">The URL parameters examples '?user=user1'</param>
        /// <param name="authToken">The authentication token.</param>
        public T GetAsync(string authToken, string identifier, string urlParams)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_serviceBaseAddress);
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authtoken", authToken);

                T profileInformationGroups = null;
                var response = httpClient.GetAsync(_subAddress + identifier + urlParams).Result;

                if (response.IsSuccessStatusCode)
                {
                    var userJson = response.Content.ReadAsStringAsync().Result;
                    profileInformationGroups = JsonConvert.DeserializeObject<T>(userJson);
                }

                return profileInformationGroups;
            }
        }

        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <param name="content">The content example object of this type FormUrlEncodedContent.</param>
        /// <param name="addressSuffix">The address suffix is complement url example 'me/change_password'.</param>
        /// <returns></returns>
        public T PostAsync(FormUrlEncodedContent content, string addressSuffix)
        {
            using (var httpClient = new HttpClient())
            {
                T result = null;
                httpClient.BaseAddress = new Uri(_serviceBaseAddress);

                var response = httpClient.PostAsync(_subAddress + addressSuffix, content).Result;
                var userJson = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<T>(userJson);
                }

                return result;
            }
        }

        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <param name="content">The content example object of this type FormUrlEncodedContent.</param>
        /// <param name="addressSuffix">The address suffix is complement url example 'me/change_password'.</param>
        /// <returns></returns>
        public string PostContentResultAsync(FormUrlEncodedContent content, string addressSuffix)
        {
            using (var httpClient = new HttpClient())
            {
                string result = "";
                httpClient.BaseAddress = new Uri(_serviceBaseAddress);

                var response = httpClient.PostAsync(_subAddress + addressSuffix, content).Result;
                

                if (response.IsSuccessStatusCode)
                {
                    var userJson = response.Content.ReadAsStringAsync().Result;
                    result = userJson;
                }

                return result;
            }
        }

        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <param name="authToken">The authentication token.</param>
        /// <param name="content">The content example object of this type FormUrlEncodedContent.</param>
        /// <param name="addressSuffix">The address suffix is complement url example 'me/change_password'.</param>
        public string PostContentResultAsync(string authToken, FormUrlEncodedContent content, string addressSuffix)
        {
            using (var httpClient = new HttpClient())
            {
                string result = "";
                httpClient.BaseAddress = new Uri(_serviceBaseAddress);
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authtoken", authToken);

                var response = httpClient.PostAsync(_subAddress + addressSuffix, content).Result;


                if (response.IsSuccessStatusCode)
                {
                    var userJson = response.Content.ReadAsStringAsync().Result;
                    result = userJson;
                }

                return result;
            }
        }

        /// <summary>
        /// Gets the list asynchronous.
        /// </summary>
        /// <param name="identifier">The identifier examples user, groups, etc</param>
        /// <param name="urlParams">The URL parameters examples '?user=user1'</param>
        /// <param name="authToken">The authentication token.</param>
        public IEnumerable<T> GetListAsync(string authToken, string identifier, string urlParams)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_serviceBaseAddress);
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authtoken", authToken);

                var response = httpClient.GetAsync(_subAddress + identifier + urlParams).Result;
                var userJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<T>>(userJson);
            }
        }


        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <param name="authToken">The authentication token.</param>
        /// <param name="content">The content example object of this type FormUrlEncodedContent.</param>
        /// <param name="addressSuffix">The address suffix is complement url example 'me/change_password'.</param>
        public T PostAsync(string authToken, FormUrlEncodedContent content, string addressSuffix)
        {
            using (var httpClient = new HttpClient())
            {
                T result = null;
                httpClient.BaseAddress = new Uri(_serviceBaseAddress);
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authtoken", authToken);

                var response = httpClient.PostAsync(_subAddress + addressSuffix, content).Result;
                var userJson = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<T>(userJson);
                }

                return result;
            }
        }
    }
}
 