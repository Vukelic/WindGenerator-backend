using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WindService_WindowsService.Models;

namespace WindService_WindowsService.Api
{
    public class WeatherProcessor
    {
        public static async Task<WeatherModel> LoadWeather(string Lat, string Lon)
        {
            //TODO:
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
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
