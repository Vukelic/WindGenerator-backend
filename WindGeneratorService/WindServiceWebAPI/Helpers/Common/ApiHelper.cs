using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WindServiceWebAPI.Helpers.Common
{
    public class ApiHelper
    {
        public static HttpClient ApiClient { get; set; }
        public static string Api_key { get; set; }
        public static void InitializeClient(string apiKey)
        {
            ApiClient = new HttpClient();

            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Api_key = apiKey;

        }
    }
}
