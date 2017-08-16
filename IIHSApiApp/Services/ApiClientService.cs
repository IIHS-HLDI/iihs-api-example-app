using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace IIHSApiApp.Services
{
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