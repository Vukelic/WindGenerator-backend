using DtoLocalServerDALImplementation;
using DtoModel.DtoModels.Implementations.WindGeneratorDevice;
using DtoModel.DtoModels.Implementations.WindGeneratorDevice_History;
using DtoServiceDAL.Abstractions;
using DtoServiceDAL.GlobalInstanceSelector;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
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
       

        Thread checkForNewGenerators_Thread;
        TimeSpan checkForNewGenerators_Thread_SleepTime = new TimeSpan(0, 0, 5);

        Thread getWindGeneratorsInfo_Thread;
        TimeSpan getWindGeneratorsInfo_Thread_SleepTime = new TimeSpan(1, 0, 0);


        bool _stop = false;

        private List<DtoWindGeneratorDevice> WindGenerators = new List<DtoWindGeneratorDevice>();

        IConfigurationRoot config;

        string connectionString;
        string api_key;

        public WindService_Service()
        {
            InitializeComponent();

            GetConfig();

            ApiHelper.InitializeClient(api_key);

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
            
            string dateStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            this.Debug_Test($"OnDebug service {dateStr}.\n ");

            OnStart(null);
            System.Threading.Thread.CurrentThread.Join();
        }
        public void GetConfig()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            config = builder.Build();
            if (config != null)
            {
                try
                {
                    var _connectionString = config.GetConnectionString("DefaultConnection");
                    if (!string.IsNullOrEmpty(_connectionString))
                    {
                        connectionString = _connectionString;
                    }

                    var web_api = config.GetSection("Api_id");
                    if (!string.IsNullOrEmpty(web_api.Value))
                    {
                        api_key = web_api.Value;
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        protected override void OnStart(string[] args)
        {
           
            this.Debug_Test($"nOnStart service.\n ");

            StartProcesses();
        }

        protected override void OnStop()
        {
            string dateStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.Debug_Test($"OnStop service, {dateStr}.\n");

            StopProcesses();
        }

        //Start & Stop processes
        private void StopProcesses()
        {
            string dateStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.Debug_Test($"StopProcesses service, {dateStr}.\n");
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

                string dateStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                this.Debug_Test($"GetGeneratorsInfo, {dateStr}.\n");

                try
                {
                    var tmpWindGenerators = GetWindGenerators();
                    this.Debug_Test($"GetGeneratorsInfo tmpWindGenerators count: {tmpWindGenerators?.Count.ToString() ?? "null"} ");
                    if (tmpWindGenerators != null)
                    {
                        foreach (var windGenerator in tmpWindGenerators)
                        {
                            WeatherModel weatherModel = GetWeatherInformationForGenerator(windGenerator);

                            if (weatherModel != null)
                            {
                                this.Debug_Test($"weatherModel not null GetGeneratorsInfo");
                                var wind_power = Calculate_WindPower(weatherModel.Current.Wind_Speed);
                                windGenerator.ValueDec = (decimal)wind_power;
                                windGenerator.ValueStr = wind_power.ToString();
                                windGenerator.TimeCreated = DateTime.UtcNow;


                                var successfulCurrent = Update_WindGenerator_CurrentPower(windGenerator);
                                this.Debug_Test($"update active generator GetGeneratorsInfo,{successfulCurrent}");
                                var successfulHistory = Add_HistoryWindGenerator(windGenerator);
                                this.Debug_Test($"add to history generator GetGeneratorsInfo,{successfulHistory}");
                            }
                        }

                        lock (WindGenerators)
                        {
                            this.Debug_Test($"lock");
                            WindGenerators = tmpWindGenerators; //for checking new generators. 
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.Debug_Test($"ERROR GetGeneratorsInfo {ex}");
                }

                Thread.Sleep(getWindGeneratorsInfo_Thread_SleepTime);
            }

        }

        //add new generators
        private void CheckForNewGenerators()
        {
            while (!_stop)
            {
                string dateStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                this.Debug_Test($"CheckForNewGenerators, {dateStr}.\n");

                try
                {
                    var tmpWindGenerators = GetWindGenerators();
                    this.Debug_Test($"CheckForNewGenerators tmpWindGenerators count: {tmpWindGenerators?.Count.ToString() ?? "null"} ");
                    if (tmpWindGenerators != null && WindGenerators != null)
                    {
                        var newGeneratorsList = tmpWindGenerators.Where(o => WindGenerators.FirstOrDefault(i => i.Id == o.Id) == null).ToList();
                        if (newGeneratorsList != null && newGeneratorsList.Count > 0)
                        {
                            foreach (var newGenerator in newGeneratorsList)
                            {
                                WeatherModel weatherModel = GetWeatherInformationForGenerator(newGenerator);

                                if (weatherModel != null)
                                {
                                    this.Debug_Test($"weather model CheckForNewGenerators not null");
                                    var wind_power = Calculate_WindPower(weatherModel.Current.Wind_Speed);
                                    newGenerator.ValueDec = (decimal)wind_power;
                                    newGenerator.ValueStr = wind_power.ToString();
                                    newGenerator.TimeCreated = DateTime.UtcNow;

                                    var successful = Update_WindGenerator_CurrentPower(newGenerator);
                                    this.Debug_Test($"update active generator CheckForNewGenerators ,{successful}");
                                    var successfulHistory = Add_HistoryWindGenerator(newGenerator);
                                    this.Debug_Test($"add to history CheckForNewGenerators,{successfulHistory}");

                                }
                            }
                            //WindGenerators.AddRange(newGeneratorsList);
                        }
                    }
                }
                catch (Exception ex)
                {

                    this.Debug_Test($"ERROR CheckForNewGenerators {ex}");
                }
                Thread.Sleep(checkForNewGenerators_Thread_SleepTime);
            }
        }

        //Work with Database functions
        private List<DtoWindGeneratorDevice> GetWindGenerators()
        {
            ADtoDAL dtoDal = GlobalDtoDALInstanceSelector.GetDtoDALImplementation?.Invoke();

            var tmpGeneratorsList = dtoDal?.GetWindGeneratorDeviceDAL()?.GetList();
            this.Debug_Test($"GetWindGenerators tmpWindGenerators count: {tmpGeneratorsList?.Value?.ToList()?.Count.ToString() ?? "null"} ");
            if (tmpGeneratorsList != null && tmpGeneratorsList.Success && tmpGeneratorsList.Value != null)
                return tmpGeneratorsList.Value.ToList();

            return null;
        }
        private void Debug_Test(string message)
        {
            // string path = "C:\\ProgramData\\windService\\debug.txt";  //for test only 
            //if (!Directory.Exists(path))
            //{
            //    Directory.CreateDirectory(path);
            //}

            //string dateStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //File.AppendAllText(path, $"{message}, {dateStr}.\n");
        }
        //Work with WeatherApi functions
        private WeatherModel GetWeatherInformationForGenerator(DtoWindGeneratorDevice newGenerator)
        {
            WeatherModel toRet = null;
            try
            {
                toRet = WeatherProcessor.LoadWeather(newGenerator.GeographicalLatitude.ToString(), newGenerator.GeographicalLongitude.ToString()).Result;
                Debug_Test("GetWeatherInformationForGenerator");
            }
            catch (Exception ex)
            {
                string dateStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Debug_Test($"ERROR GetWeatherInformationForGenerator {ex}");
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

            if (Wind_Speed < 3)
            {
                toRet = 0;
            }
            else if (Wind_Speed >= 3 && Wind_Speed <= 10)
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

                if (windGeneratorResponse != null && windGeneratorResponse.Success)
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
