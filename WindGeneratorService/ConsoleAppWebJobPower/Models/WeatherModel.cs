﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppWebJobPower.Models
{
    public class WeatherModel
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
        public CurrentWeatherModel Current { get; set; }
        public List<DailyModel> Daily { get; set; }
        public List<HourlyModel> Hourly { get; set; }

    }
}
