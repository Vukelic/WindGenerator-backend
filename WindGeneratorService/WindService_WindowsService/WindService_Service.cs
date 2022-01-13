using DtoLocalServerDALImplementation;
using DtoModel.DtoModels.Implementations.WindGeneratorDevice;
using DtoModel.DtoModels.Implementations.WindGeneratorDevice_History;
using DtoServiceDAL.Abstractions;
using DtoServiceDAL.GlobalInstanceSelector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindService_WindowsService.Api;
using WindService_WindowsService.Models;

namespace WindService_WindowsService
{
    partial class WindService_Service : ServiceBase
    {
        string connectionString = @"Server=DESKTOP-H4344E1\SQLEXPRESS;Database=wind-service-database;Trusted_Connection=True;MultipleActiveResultSets=true";

        Thread checkForNewGenerators_Thread;
        TimeSpan checkForNewGenerators_Thread_SleepTime = new TimeSpan(0, 0, 5);

        Thread getWindGeneratorsInfo_Thread;
        TimeSpan getWindGeneratorsInfo_Thread_SleepTime = new TimeSpan(1, 0, 0);


        bool _stop = false;
        bool _gettingGeneratorInfoProccessStarted = false;

        private List<DtoWindGeneratorDevice> WindGenerators = new List<DtoWindGeneratorDevice>();

        public WindService_Service()
        {
            InitializeComponent();
            //DTO dal implementation
            GlobalDtoDALInstanceSelector.GetDtoDALImplementation = () =>
            {
                return new DtoLocalServiceDAL(connectionString);
            };
            InitializeClass();
           
        }

        private void InitializeClass()
        {
            WindGenerators = GetWindGenerators();
        }

        public void OnDebug()
        {
            OnStart(null);
            System.Threading.Thread.CurrentThread.Join();
        }

        protected override void OnStart(string[] args)
        {
            StartProcesses();
        }

        protected override void OnStop()
        {
            StopProcesses();
        }

        //Start & Stop processes
        private void StopProcesses()
        {
            _stop = true;

            // checkForNewGenerators_Thread = null;
            // getWindGeneratorsInfo_Thread = null;
        }

        private void StartProcesses()
        {
            _stop = false;

            checkForNewGenerators_Thread = new Thread(new ThreadStart(CheckForNewGenerators));
            checkForNewGenerators_Thread.Start();

            getWindGeneratorsInfo_Thread = new Thread(new ThreadStart(GetGeneratorsInfo));
            getWindGeneratorsInfo_Thread.Start();

        }

        //Threads functions
        //add new and add history
        private void GetGeneratorsInfo()
        {
            while (!_stop)
            {

                var tmpWindGenerators = GetWindGenerators();
                if (tmpWindGenerators != null)
                {
                    foreach (var windGenerator in tmpWindGenerators)
                    {
                        WeatherModel weatherModel = GetWeatherInformationForGenerator(windGenerator);
                        if (weatherModel != null)
                        {
                            var wind_power = Calculate_WindPower(weatherModel.Current.Wind_Speed);
                            windGenerator.ValueDec = (decimal)wind_power;
                            windGenerator.ValueStr = wind_power.ToString();
                            windGenerator.TimeCreated = DateTime.UtcNow;


                            var successfulCurrent = Update_WindGenerator_CurrentPower(windGenerator);

                            var successfulHistory = Add_HistoryWindGenerator(windGenerator);
                        }
                    }

                    lock (WindGenerators)
                    {
                        WindGenerators = tmpWindGenerators; //for checking new generators. 
                    }
                }

                Thread.Sleep(getWindGeneratorsInfo_Thread_SleepTime);
            }

        }

        //add new generators
        private void CheckForNewGenerators()
        {
            while (!_stop)
            {
                var tmpWindGenerators = GetWindGenerators();
                if (tmpWindGenerators != null && WindGenerators != null)
                {
                    var newGeneratorsList = tmpWindGenerators.Where(o => WindGenerators.FirstOrDefault(i => i.Id == o.Id) == null).ToList();
                    if (newGeneratorsList != null && newGeneratorsList.Count > 0)
                    {
                        foreach (var newGenerator in newGeneratorsList)
                        {
                            WeatherModel weatherModel =  GetWeatherInformationForGenerator(newGenerator);

                            if (weatherModel != null)
                            {
                                var wind_power = Calculate_WindPower(weatherModel.Current.Wind_Speed);
                                newGenerator.ValueDec = (decimal)wind_power;
                                newGenerator.ValueStr = wind_power.ToString();
                                newGenerator.TimeCreated = DateTime.UtcNow;

                                var successful = Update_WindGenerator_CurrentPower(newGenerator);
                                var successfulHistory = Add_HistoryWindGenerator(newGenerator);
                            }
                        }
                        //WindGenerators.AddRange(newGeneratorsList);
                    }
                }
                Thread.Sleep(checkForNewGenerators_Thread_SleepTime);
            }
        }



        //Work with Database functions
        private List<DtoWindGeneratorDevice> GetWindGenerators()
        {
            ADtoDAL dtoDal = GlobalDtoDALInstanceSelector.GetDtoDALImplementation?.Invoke();

            var tmpGeneratorsList = dtoDal?.GetWindGeneratorDeviceDAL()?.GetList();
            if (tmpGeneratorsList != null && tmpGeneratorsList.Success && tmpGeneratorsList.Value != null)
                return tmpGeneratorsList.Value.ToList();

            return null;
        }

        //Work with WeatherApi functions
        private WeatherModel GetWeatherInformationForGenerator(DtoWindGeneratorDevice newGenerator)
        {
            WeatherModel toRet = null;
            try
            {
                toRet = WeatherProcessor.LoadWeather(newGenerator.GeographicalLatitude.ToString(), newGenerator.GeographicalLongitude.ToString()).Result;
            }
            catch (Exception ex)
            {

            }
            return toRet;
        }

        //Calculate wind power
        private double Calculate_WindPower(double Wind_Speed)
        {
            double toRet = 0;
            const double p_normal = 103.44;
            const double ro = 1.293;
            const double A = 80;

            if(Wind_Speed < 3)
            {
                toRet = 0;
            }else if (Wind_Speed >= 3 && Wind_Speed <= 10)
            {
                toRet = 0.5 * A * ro * Math.Pow(Wind_Speed, 3);
            }
            else if (Wind_Speed >= 10 && Wind_Speed <= 20)
            {
                toRet = p_normal;
            }
            else if (Wind_Speed >= 20)
            {
                toRet = 0;
            }
            else
            {
                // default value 0
            }

            Math.Round(toRet, 2);
            return toRet;
        }
        
        //add wind power in database
        private bool Update_WindGenerator_CurrentPower(DtoWindGeneratorDevice windGenerator)
        {
            ADtoDAL dtoDal = GlobalDtoDALInstanceSelector.GetDtoDALImplementation?.Invoke();

            if (windGenerator != null)
            {
                var windGeneratorResponse = dtoDal?.GetWindGeneratorDeviceDAL()?.UpdatePowerOnGenerator(windGenerator);

                if(windGeneratorResponse != null && windGeneratorResponse.Success)
                {
                    return true;
                }
            }
            return false;
        }

        private bool Add_HistoryWindGenerator(DtoWindGeneratorDevice windGenerator)
        {
            ADtoDAL dtoDal = GlobalDtoDALInstanceSelector.GetDtoDALImplementation?.Invoke();

            if (windGenerator != null)
            {
                var historyWindGenerator = new DtoWindGeneratorDevice_History();
                historyWindGenerator.TimeCreated = DateTime.UtcNow;
                historyWindGenerator.GeographicalLatitude = windGenerator.GeographicalLatitude;
                historyWindGenerator.GeographicalLatitudeStr = windGenerator.GeographicalLatitudeStr;
                historyWindGenerator.GeographicalLongitude = windGenerator.GeographicalLongitude;
                historyWindGenerator.GeographicalLongitudeStr = windGenerator.GeographicalLongitudeStr;
                historyWindGenerator.Country = windGenerator.Country;
                historyWindGenerator.City = windGenerator.City;
                historyWindGenerator.Description = windGenerator.Description;
                historyWindGenerator.ParentWindGeneratorDeviceId = windGenerator.Id;
                historyWindGenerator.ValueDec = windGenerator.ValueDec;
                historyWindGenerator.ValueStr = windGenerator.ValueStr;
                historyWindGenerator.Name = windGenerator.Name;


                var windGeneratorResponse = dtoDal?.GetWindGeneratorDevice_HistoryDAL()?.Create(historyWindGenerator);

                if (windGeneratorResponse != null && windGeneratorResponse.Success)
                {
                    return true;
                }
            }
            return false;
        }
    }//[Class]
}//[Namespace]
