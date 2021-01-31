using MMT.CustomerOrder.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MMT.CustomerOrder.Api
{
    public interface IUserApi
    {
        Task<Models.Customer> GetUserAsync(string email);
    }
    public class UserApi : IUserApi
    {
        private IUrlBuilder _urlBuilder;
        private HttpClient _client;
        public UserApi(IUrlBuilder urlBuilder)
        {
            _urlBuilder = urlBuilder;
            _client = new HttpClient();
        }
        public async Task<Models.Customer> GetUserAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new AppException("User email required");
            var parameters = new Dictionary<string, string>();
            parameters.Add("email", email);
            var response=await _client.GetAsync(_urlBuilder.Build("GetUserDetails",parameters));
            if (!response.IsSuccessStatusCode)
                throw new AppException($"Invalid email {email}");
            return JsonConvert.DeserializeObject<Models.Customer>(await response.Content.ReadAsStringAsync());
        }
    }
    
}
