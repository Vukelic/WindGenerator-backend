using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WindServiceWebAPI.Helpers.Common.Models.CurrentWeather
{
    public class TempDailyModel
    {
        public double Day { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
        public double Night { get; set; }
        public double Eve { get; set; }
        public double Morn { get; set; }
    }
}
