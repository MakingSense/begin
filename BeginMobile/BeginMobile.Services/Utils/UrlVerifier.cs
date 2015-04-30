using System.Net.Http;
using System.Threading.Tasks;

namespace BeginMobile.Services.Utils
{
    public class UrlVerifier
    {
        public static async Task<bool> UrlExists(string url)
        {
            using (var client = new HttpClient())
            {
                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Head, url);
                var response = await client.SendAsync(httpRequestMessage);
                return response.IsSuccessStatusCode;
            }
        }
    }
}