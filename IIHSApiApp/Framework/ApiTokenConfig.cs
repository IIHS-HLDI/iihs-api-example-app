using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace IIHSApiApp
{
    public static class ApiTokenConfig
    {
        public static string ApiTokenKey = "";
        static string accountName = "";
        static string apiKey = "";

        static ApiTokenConfig()
        {
            accountName = "mrpickles2009";
            apiKey = ApiConfig.ApiKey;

            var nonce = new byte[8];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(nonce);

            DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var expiryTime = DateTime.UtcNow.AddMinutes(30);
            var seconds = (long)((expiryTime - Epoch).TotalSeconds);
            var expiryBytes = BitConverter.GetBytes(seconds);

            var accountNameBytes = Encoding.UTF8.GetBytes(accountName);
            var apiKeyBytes = Encoding.UTF8.GetBytes(apiKey);
            var bytesToHash = accountNameBytes
                .Concat(apiKeyBytes)
                .Concat(nonce)
                .Concat(expiryBytes)
                .ToArray();
            var hash = new System.Security.Cryptography.SHA1CryptoServiceProvider().ComputeHash(bytesToHash);

            var token = expiryBytes
                .Concat(nonce)
                .Concat(hash).ToArray();
            var tokenString = (Convert.ToBase64String(token));
            tokenString = tokenString.Replace('+', '-').Replace('/', '_').Replace("=", "");

            ApiTokenKey = tokenString;
        }
    }
}