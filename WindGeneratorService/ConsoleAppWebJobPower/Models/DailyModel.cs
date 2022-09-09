using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace ConsoleAppWebJobPower.Models
{
    public class DailyModel : INotifyPropertyChanged
    {
        public int Dt { get; set; }
        public int Sunrinse { get; set; }
        public int Sunset { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public double Wind_Speed { get; set; }

        #region -Temp- property
        private DailyTemp _Temp;
        public DailyTemp Temp
        {
            get { return _Temp; }
            set
            {
                if (_Temp != value)
                {
                    _Temp = value;
                    GetMinMax();
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -Weather- property
        private List<WeatherDescriptionModel> _Weather;
        public List<WeatherDescriptionModel> Weather
        {
            get { return _Weather; }
            set
            {
                if (_Weather != value)
                {
                    _Weather = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion


        //UI using
        #region -maxCurrentDayTemp- property
        private string _maxCurrentDayTemp;
        public string maxCurrentDayTemp
        {
            get { return _maxCurrentDayTemp; }
            set
            {
                if (_maxCurrentDayTemp != value)
                {
                    _maxCurrentDayTemp = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -minCurrentDayTemp- property
        private string _minCurrentDayTemp;
        public string minCurrentDayTemp
        {
            get { return _minCurrentDayTemp; }
            set
            {
                if (_minCurrentDayTemp != value)
                {
                    _minCurrentDayTemp = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -DayName- property
        private String _DayName;
        public String DayName
        {
            get { return convertDtToDateTime(); }
            set
            {
                if (_DayName != value)
                {
                    _DayName = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -weatherDescription- property
        private String _weatherDescription;
        public String weatherDescription
        {
            get { return getWetherDescription(); }
            set
            {
                if (_weatherDescription != value)
                {
                    _weatherDescription = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion




        //functions

        private void GetMinMax()
        {
            if (Temp != null)
            {
                maxCurrentDayTemp = Math.Round(Temp.Max, 1) + "°C";
                minCurrentDayTemp = Math.Round(Temp.Min, 1) + "°C";
            }
        }




        public string convertDtToDateTime()
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(this.Dt).ToLocalTime();
            return dtDateTime.DayOfWeek.ToString() + ",\n " + dtDateTime.ToString("MMMM dd").First().ToString().ToUpper() + dtDateTime.ToString("MMMM dd").Substring(1);

        }

        public string getWetherDescription()
        {
            if (Weather != null && Weather.Count > 0)
            {
                return Weather.FirstOrDefault().Description.First().ToString().ToUpper() + Weather.FirstOrDefault().Description.Substring(1);
            }
            return "";
        }

        #region INotifyPropertyChange implementation
        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
