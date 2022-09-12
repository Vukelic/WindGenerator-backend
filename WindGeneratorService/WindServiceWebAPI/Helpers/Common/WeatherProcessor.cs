using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WindServiceWebAPI.Helpers.Common.Models.CurrentWeather;

namespace WindServiceWebAPI.Helpers.Common
{
    public class WeatherProcessor
    {
      
        public static async Task<HistoryModel> LoadHistoryWeather(string Lat, string Lon)
        {

            string url = $"https://history.openweathermap.org/data/2.5/aggregated/year?lat={ Lat }&lon={ Lon }&appid={ApiHelper.Api_key}";
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

        public static async Task<WeatherModel> LoadWeather(string Lat, string Lon)
        {
 
            string url = $"https://api.openweathermap.org/data/2.5/onecall?lat={ Lat }&lon={ Lon }&units=metric&APPID={ApiHelper.Api_key}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    WeatherModel weather = await response.Content.ReadAsAsync<WeatherModel>();
                    return weather;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
