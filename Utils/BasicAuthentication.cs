using System;
using System.Text;

namespace WebApplication.Utils
{
    public class BasicAuthentication
    {
        private static readonly string _username = "Admin";
        private static readonly string _password = "Admin";


        public static bool Authenticate(System.Net.Http.HttpRequestMessage request)
        {
            string authHeader = request.Headers.Authorization.ToString();
            if (authHeader != null && authHeader.StartsWith("Basic"))
            {
                // Extract credentials
                string encodedCredentials = authHeader.Substring("Basic ".Length).Trim();
                byte[] decodedBytes = Convert.FromBase64String(encodedCredentials);
                string decodedCredentials = Encoding.UTF8.GetString(decodedBytes);
                string[] parts = decodedCredentials.Split(':');
                string username = parts[0];
                string password = parts[1];

                // Check credentials
                if (username == _username && password == _password)
                {
                    return true;
                }
            }

            return false;
        }
    }
}