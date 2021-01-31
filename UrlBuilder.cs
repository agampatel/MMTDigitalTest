using Microsoft.Extensions.Configuration;
using MMT.CustomerOrder.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace MMT.CustomerOrder
{
    

    public interface IUrlBuilder
    {
        string Build(string route);
        string Build(string route, Dictionary<string, string> parameters);
    }
    public class UrlBuilder : IUrlBuilder
    {
        private const string BASE_URL = "BaseUrl";
        private const string API_KEY = "ApiKey";

        private IConfiguration _configuration;
        public UrlBuilder(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Build(string route)
        {
            ValidateRequiredParameters();
            var coll = new Dictionary<string,string>();
            coll.Add("code", _configuration[API_KEY]);
            return $"{BuildBaseUrl()}/{route}?{BuildQueryString(coll)}";
        }
        public string Build(string route, Dictionary<string, string> parameters)
        {
            ValidateRequiredParameters();
            var coll = new Dictionary<string, string>();
            coll.Add("code", _configuration[API_KEY]);
            var apiQuery = BuildQueryString(coll);

            var paramsQuery = BuildQueryString(parameters);
            paramsQuery = paramsQuery == null ? string.Empty : "&" + paramsQuery;

            return $"{BuildBaseUrl()}/{route}?{apiQuery}{paramsQuery}";
        }

        private string _baseUrl;
        private string BuildBaseUrl()
        {
            if(_baseUrl == null)
            {
                _baseUrl = $"{_configuration[BASE_URL]}";
            }
            return _baseUrl;
        }
        private void ValidateRequiredParameters()
        {
            if (string.IsNullOrWhiteSpace(_configuration[BASE_URL]))
                throw new UrlMissingException();
            if (string.IsNullOrWhiteSpace(_configuration[API_KEY]))
                //Can create new exception type for this
                throw new AppException("Api Key is not configured");
        }

        public string BuildQueryString(Dictionary<string,string> items)
        {
            if (items == null) return null;
            return string.Join("&",items.Select(x => $"{x.Key}={x.Value}"));
        }

        
    }
}
