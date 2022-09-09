using ConsoleAppWebJobHistory.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppWebJobHistory.Helpers
{
    public class WeatherProcessor
    {
        public static async Task<WeatherModel> LoadWeather(string Lat, string Lon)
        {
            //TODO:
            //
            string url = $"https://api.openweathermap.org/data/2.5/weather?lat={ Lat }&lon={ Lon }&APPID={ApiHelper.Api_key}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var resp = await response.Content.ReadAsStringAsync();
                  

                    dynamic data = JsonConvert.DeserializeObject(resp);
                   
                    dynamic wind = data[5];

                    WeatherModel weather = new WeatherModel();
                    weather.Current.Wind_Speed = wind.speed;

                    return weather;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<WeatherModel> LoadHistoryWeather(string Lat, string Lon, long start, long end)
        {
            //TODO:
            string url = $"https://api.openweathermap.org/data/2.5/onecall?lat={ Lat }&lon={ Lon }&type=hour&start={ start }&end={ end }&APPID={ApiHelper.Api_key}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    WeatherModel weather = await response.Content.ReadAsAsync<WeatherModel>();
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
