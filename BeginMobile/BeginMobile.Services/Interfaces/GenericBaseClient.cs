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
    public class GenericBaseClient<T, TResourceIdentifier, TUrlParams> where T : class
    {
        private HttpClient _httpClient;
        protected readonly string ServiceBaseAddress;
        private readonly string _subAddress;
        private bool _disposed = false;

        public GenericBaseClient(string baseAddress, string subAddress)
        {
            ServiceBaseAddress = baseAddress;
            _subAddress = subAddress;
            _httpClient = MakeHttpClient(ServiceBaseAddress);
        }

        protected virtual HttpClient MakeHttpClient(string serviceBaseAddress)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(serviceBaseAddress);

            return _httpClient;
        }

        public void SetAuthToken(string authtoken)
        {
            if (_httpClient != null)
            {
                if (!string.IsNullOrEmpty(authtoken))
                {
                    _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authtoken", authtoken);
                }
            }
            
        }

        public void RemoveAuthToken()
        {
            if (_httpClient != null)
            {
                if (_httpClient.DefaultRequestHeaders.Contains("authtoken"))
                {
                    _httpClient.DefaultRequestHeaders.Remove("authtoken");
                }
            }
        }

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="identifier">The identifier examples user, groups, etc</param>
        /// <param name="urlParams">The URL parameters examples '?user=user1' </param>
        /// <returns>return class T</returns>
        public T GetAsync(TResourceIdentifier identifier, TUrlParams urlParams)
        {
            T profileInformationGroups = null;
            var response = _httpClient.GetAsync(_subAddress + identifier + urlParams).Result;

            if (response.IsSuccessStatusCode)
            {
                var userJson = response.Content.ReadAsStringAsync().Result;
                profileInformationGroups = JsonConvert.DeserializeObject<T>(userJson);
            }

            RemoveAuthToken();
            return profileInformationGroups;
        }

        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <param name="content">The content example object of this type FormUrlEncodedContent.</param>
        /// <param name="addressSuffix">The address suffix is complement url example 'me/change_password'.</param>
        /// <returns></returns>
        public T PostAsync(FormUrlEncodedContent content, TResourceIdentifier addressSuffix)
        {
            T result = null;

            var response = _httpClient.PostAsync(_subAddress + addressSuffix.ToString(), content).Result;
            var userJson = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<T>(userJson);
            }

            return result;
        }

        /// <summary>
        /// Gets the list asynchronous).
        /// </summary>
        /// <param name="identifier">The identifier examples user, groups, etc</param>
        /// <param name="urlParams">The URL parameters examples '?user=user1' </param>
        /// <returns>return class T</returns>
        public IEnumerable<T> GetListAsync(TResourceIdentifier identifier, TUrlParams urlParams)
        {
            IEnumerable<T> groups;

            var response = _httpClient.GetAsync(_subAddress + identifier.ToString() + urlParams.ToString()).Result;
            var userJson = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                groups = JsonConvert.DeserializeObject<List<T>>(userJson);
            }
            else
            {
                groups = JsonConvert.DeserializeObject<List<T>>(userJson);
            }

            RemoveAuthToken();
            return groups;
        }
    }
}
 