using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Auth0.Core.Exceptions;
using Olive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website
{
    public class Auth0Helper
    {
        static readonly AuthenticationApiClient ApiClient;
        static readonly string ClientId;
        static readonly string ClientSecret;
        static readonly string Connection = "Username-Password-Authentication";

        static Auth0Helper()
        {
            ApiClient = new AuthenticationApiClient(new Uri($"https://{Config.Get("Authentication:Auth0:Domain")}/"));
            ClientId = Config.Get("Authentication:Auth0:ClientId");
            ClientSecret = Config.Get("Authentication:Auth0:ClientSecret");
        }
        
        public static async Task<AuthenticationResult> Authenticate(string username, string password)
        {
            try
            {
                var result = await ApiClient.UsernamePasswordLoginAsync(new UsernamePasswordLoginRequest
                {
                    ClientId = ClientId,
                    Scope = "openid profile",
                    Username = username,
                    Password = password,
                    Connection = Connection
                });

                return new AuthenticationResult() { Success = true };
            }
            catch (ApiException exception)
            {
                return new AuthenticationResult() { Message = exception.Message };
            }
        }

        public class AuthenticationResult
        {
            public bool Success { get; set; }
            public string Message { get; set; }
        }
    }
}
