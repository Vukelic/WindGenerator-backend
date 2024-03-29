﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WindService_WindowsService.Models
{
    public class CityModel : INotifyPropertyChanged
    {

        #region -Name- property
        private String _Name;
        public String Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -coord- property
        private CoordinateModel _coord;
        public CoordinateModel coord
        {
            get { return _coord; }
            set
            {
                if (_coord != value)
                {
                    _coord = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -CityWeather- property
        private WeatherModel _CityWeather;
        public WeatherModel CityWeather
        {
            get { return _CityWeather; }
            set
            {
                if (_CityWeather != value)
                {
                    _CityWeather = value;
                    GetCurrentTemp();
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion


        // properties for work in MainWindow
        #region -IsChecked- property
        private bool _IsChecked;
        public bool IsChecked
        {
            get { return _IsChecked; }
            set
            {
                if (_IsChecked != value)
                {
                    _IsChecked = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -IsRenderedOnGraph- property
        private bool _IsRenderedOnGraph = true;
        public bool IsRenderedOnGraph
        {
            get { return _IsRenderedOnGraph; }
            set
            {
                if (_IsRenderedOnGraph != value)
                {
                    _IsRenderedOnGraph = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion


        #region -CurrentTemp- property
        private double _CurrentTemp;
        public double CurrentTemp
        {
            get { return _CurrentTemp; }
            set
            {
                if (_CurrentTemp != value)
                {
                    _CurrentTemp = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -maxCurrentDayTemp- property
        private double _maxCurrentDayTemp;
        public double maxCurrentDayTemp
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
        private double _minCurrentDayTemp;
        public double minCurrentDayTemp
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






        private void GetCurrentTemp()
        {
            if (CityWeather != null && CityWeather.Current != null && CityWeather.Daily != null && CityWeather.Daily.Count > 1)
            {
                CurrentTemp = Math.Round(CityWeather.Current.Temp, 1);

                maxCurrentDayTemp = Math.Round(CityWeather.Daily[1].Temp.Max, 1);
                minCurrentDayTemp = Math.Round(CityWeather.Daily[1].Temp.Min, 1);
            }
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
