using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace IIHSApiApp.Services
{
    /// <summary>
    /// Uses an http client to make requests to http://api.iihs.org/v4.  
    /// </summary>
    public class ApiClientService
    {
        HttpClient client;
        private string rootUrl;
        
        public ApiClientService(string apiKey)
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("IIHS-apikey", apiKey);

            rootUrl = "https://api.iihs.org/v4/";
        }

        /// <summary>
        /// Requests json responses from server and deserializes them into the provided datatype.  The provided datatype should match the json response object shape for it to be successful.
        /// </summary>
        /// <typeparam name="T">DataType that json response is deserialized to</typeparam>
        /// <param name="url">The api v4 service call url.  For example "ratings/all-classes"</param>
        /// <returns>Typed result of web service request</returns>
        public async Task<T> GetAsync<T>(string url)
        {
            var apiUrl = $"{this.rootUrl}{url}";

            T result = default(T);

            HttpResponseMessage response = await client.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var stringResult = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<T>(stringResult);
            }
            return result;
        }
    }
}