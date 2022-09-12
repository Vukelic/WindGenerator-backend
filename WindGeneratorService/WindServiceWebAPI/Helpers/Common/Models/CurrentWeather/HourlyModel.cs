using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WindServiceWebAPI.Helpers.Common.Models.CurrentWeather
{
    public class HourlyModel
    {
        public int Dt { get; set; }
        //public double Temp { get; set; }
        #region -Temp- property
        private double _Temp;
        public double Temp
        {
            get { return _Temp; }
            set
            {
                if (_Temp != value)
                {
                    _Temp = value;
                    //NotifyPropertyChanged();
                }
            }
        }
        #endregion
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public double Wind_Speed { get; set; }
        public List<WeatherDescriptionModel> Weather { get; set; }
    }
}
