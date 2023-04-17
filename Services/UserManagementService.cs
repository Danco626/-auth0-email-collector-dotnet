using System;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using DotNetRedirect.Models;
using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Xml.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace DotNetRedirect.Services
{
    public interface IUserManagementService
    {
        public Task<Boolean> UpdateEmail(string userId, string email);
    }


    public class UserManagementService: IUserManagementService
    {        
        Auth0Config _configuration;
        private HttpClient client = new HttpClient();

        public UserManagementService(IOptions<Auth0Config> options)
		{
            _configuration = options.Value;

        }

        private async Task<ClientCredentialsResponse> GetMgmtApiToken() {
            var clientCredentialsRequest = new ClientCredentialsRequest {
                client_id = _configuration.MgmtClientId,
                client_secret = _configuration.MgmtClientSecret,
                audience = $"https://{_configuration.Auth0Domain}/api/v2/"
            };

            var clientCredentialsResponse = await client.PostAsJsonAsync<ClientCredentialsRequest>($"https://{_configuration.Domain}/oauth/token", clientCredentialsRequest);
            return await clientCredentialsResponse.Content.ReadFromJsonAsync<ClientCredentialsResponse>();         
        }

        public async Task<Boolean> UpdateEmail(string userId, string email)
        {
            var clienCredentialsResponse = await GetMgmtApiToken();
            var userUpdateRequest = new UpdateUserRequest(email);          
            var stringContent = new StringContent(JsonConvert.SerializeObject(userUpdateRequest), Encoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", clienCredentialsResponse.access_token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var emailUpdateResult = await client.PatchAsync($"https://{_configuration.Domain}/api/v2/users/{userId}", stringContent);
            return emailUpdateResult.IsSuccessStatusCode;
            //return await emailUpdateResult.Content.ReadFromJsonAsync<UpdateUserResponse>();
        }

    }
}

