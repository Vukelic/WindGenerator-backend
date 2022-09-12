using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WindServiceWebAPI.Helpers.Common
{
    public class WeatherProcessor
    {
      
        public static async Task<HistoryModel> LoadHistoryWeather(string Lat, string Lon)
        {
            //TODO:
            string url = $"https://history.openweathermap.org/data/2.5/aggregated/year?lat={ Lat }&lon={ Lon }&appid={ApiHelper.Api_key}";
            //https://history.openweathermap.org/data/2.5/aggregated/year?lat=35&lon=139&appid={API key}
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var tmp = await response.Content.ReadAsStringAsync();
                    HistoryModel weather = await response.Content.ReadAsAsync<HistoryModel>();
                    return weather;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
