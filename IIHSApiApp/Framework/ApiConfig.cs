using System.IO;
using System.Linq;
using System.Reflection;

namespace IIHSApiApp
{
    public static class ApiConfig
    {
        public static string ApiKey = "";
        public static string MakesCacheKey = "makes";
        public static string ClassesCacheKey = "classes";

        static ApiConfig()
        {
            var assembly = typeof(ApiConfig).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("IIHSApiApp.api-key.txt");
            string apiKeyText = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                apiKeyText = reader.ReadToEnd();
            }

            ApiKey = apiKeyText;
        }
    }
}