using ConsoleAppWebJobPower.Helpers;
using ConsoleAppWebJobPower.Models;
using DtoLocalServerDALImplementation;
using DtoModel.DtoModels.Implementations.WindGeneratorDevice;
using DtoServiceDAL.Abstractions;
using DtoServiceDAL.GlobalInstanceSelector;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleAppWebJobPower
{
    public class PowerService
    {


        Thread checkForNewGenerators_Thread;
        TimeSpan checkForNewGenerators_Thread_SleepTime = new TimeSpan(3, 0, 0);

        bool _stop = false;

        IConfigurationRoot config;

        string connectionString;
        string api_key;

        public PowerService()
        {
            #region apis
            // connectionString = "Data Source=tcp:wind-service-database2.database.windows.net,1433;Initial Catalog=WindServiceWebAPI_db;User Id=tmp@wind-service-database2;Password=SuperAdmin!1";
            connectionString = "Server=tcp:wind-service-database2.database.windows.net,1433;Initial Catalog=WindServiceWebAPI_db;Persist Security Info=False;User ID=tmp;Password=SuperAdmin!1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
           // connectionString = "Server=DESKTOP-H4344E1\\SQLEXPRESS;Database=wind-service-database2;Trusted_Connection=True;MultipleActiveResultSets=true";
            api_key = "a2a055dbb982179b05c3eb6481fbb9db";
            #endregion
            ApiHelper.InitializeClient(api_key);

            GlobalDtoDALInstanceSelector.GetDtoDALImplementation = () =>
            {
                return new DtoLocalServiceDAL(connectionString);
            };


        }
        public void OnDebug()
        {
            Console.WriteLine("OnDebug");
            string dateStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


            OnStart();
       //    System.Threading.Thread.CurrentThread.Join();
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
        private void OnStart()
        {

            //this.Debug_Test($"nOnStart service.\n ");
            Console.WriteLine("OnStart");
            StartProcesses();
        }
        private void OnStop()
        {
            string dateStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //this.Debug_Test($"OnStop service, {dateStr}.\n");

            StopProcesses();
        }

        //Start & Stop processes
        private void StopProcesses()
        {
            string dateStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //this.Debug_Test($"StopProcesses service, {dateStr}.\n");
            _stop = true;
        }

        private void StartProcesses()
        {
            _stop = false;
            Console.WriteLine("StartProcesses");
            checkForNewGenerators_Thread = new Thread(new ThreadStart(CheckForNewGenerators));
            checkForNewGenerators_Thread.Start();

            //CheckForNewGenerators();
        }

        //Threads functions

        //add new generators
        private void CheckForNewGenerators()
        {
            Console.WriteLine("CheckForNewGenerators");
            try
            {
                ADtoDAL dtoDal = GlobalDtoDALInstanceSelector.GetDtoDALImplementation?.Invoke();

                while (!_stop)
                {
                    Console.WriteLine($"whileee");
                    try
                    {
                        var tmpWindGenerators = GetWindGenerators();
                        //Console.WriteLine($"tmpWindGenerators {tmpWindGenerators.Count}");
                        if (tmpWindGenerators != null)
                        {

                            if (tmpWindGenerators != null && tmpWindGenerators.Count > 0)
                            {
                                foreach (var newGenerator in tmpWindGenerators)
                                {

                                    WeatherModel weatherModel = GetWeatherInformationForGenerator(newGenerator);
                                    var type = dtoDal?.GetWindGeneratorTypeDAL()?.Get(newGenerator.ParentWindGeneratorTypeId);
                                    if (weatherModel != null)
                                    {
                                        Console.WriteLine("weatherModel not null");
                                        //this.Debug_Test($"weather model CheckForNewGenerators not null");
                                        var wind_power = Calculate_WindPower(weatherModel.Current.Wind_Speed, type.Value.PowerOfTurbines);
                                        newGenerator.ValueDec = (decimal)wind_power;
                                        newGenerator.ValueStr = wind_power.ToString();
                                        newGenerator.AdditionalJsonData = weatherModel.Daily[0].Wind_Speed.ToString();
                                        newGenerator.TimeCreated = DateTime.UtcNow;


                                        //this.Debug_Test($"update active generator CheckForNewGenerators ,{successful}");


                                    }
                                    else
                                    {
                                        Console.WriteLine("weatherModel is null");
                                    }
                                    var successful = Update_WindGenerator_CurrentPower(newGenerator);
                                }

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"ex {ex.Message}");
                        //this.Debug_Test($"ERROR CheckForNewGenerators {ex}");
                    }
                    Thread.Sleep(checkForNewGenerators_Thread_SleepTime);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR CheckForNewGenerators {ex}");
            }
        }

        //Work with Database functions
        private List<DtoWindGeneratorDevice> GetWindGenerators()
        {
            Console.WriteLine("GetWindGenerators");
            try
            {
                ADtoDAL dtoDal = GlobalDtoDALInstanceSelector.GetDtoDALImplementation?.Invoke();

                var tmpGeneratorsList = dtoDal?.GetWindGeneratorDeviceDAL()?.GetList();
  
             //   Console.WriteLine($"val {tmpGeneratorsList.Value}");
             //   Console.WriteLine($"mess {tmpGeneratorsList.Message}");
             //   Console.WriteLine($"desc {tmpGeneratorsList.MessageDescription}");
             //   Console.WriteLine($"succ {tmpGeneratorsList.Success}");
               // //this.Debug_Test($"GetWindGenerators tmpWindGenerators count: {tmpGeneratorsList?.Value?.ToList()?.Count.ToString() ?? "null"} ");
                if (tmpGeneratorsList != null && tmpGeneratorsList.Success && tmpGeneratorsList.Value != null)
                    return tmpGeneratorsList.Value.ToList();

               
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR GetWindGenerators {ex}");
            }

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
                if(toRet == null)
                {
                    Console.WriteLine($"Wheater API NOT WORKING");
                }
                else
                {
                    Console.WriteLine($"Wheater API  WORKING");
                }
                
                Debug_Test("GetWeatherInformationForGenerator");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wheater API NOT  WORKING {ex.Message}");
            }
            return toRet;
        }

        //Calculate wind power
        private double Calculate_WindPower(double Wind_Speed, string power = null)
        {
             //m/s
            double toRet = 0;
            double p_normal = Convert.ToDouble(power);
            const double ro = 1.293;
            const double A = 80;

            if (Wind_Speed < 3)
            {
                toRet = 0;
            }
            else if (Wind_Speed >= 3 && Wind_Speed <= 10)
            {
                var v3 = Math.Pow(Wind_Speed, 3);
                toRet = (0.5 * A * ro * v3) ; //to W
            }
            else if (Wind_Speed >= 10 && Wind_Speed <= 20)
            {
                toRet = p_normal;
            }
            else if (Wind_Speed >= 20)
            {
                toRet = p_normal;
            }
            else
            {
                // default value 0
            }

            toRet = Math.Round(toRet, 2);
            return toRet;
        }

        //add wind power in database
        private bool Update_WindGenerator_CurrentPower(DtoWindGeneratorDevice windGenerator)
        {
            ADtoDAL dtoDal = GlobalDtoDALInstanceSelector.GetDtoDALImplementation?.Invoke();

            if (windGenerator != null)
            {
                windGenerator.TimeModified = DateTime.UtcNow;
                var windGeneratorResponse = dtoDal?.GetWindGeneratorDeviceDAL()?.UpdatePowerOnGenerator(windGenerator);

                if (windGeneratorResponse != null && windGeneratorResponse.Success)
                {
                    return true;
                }
            }
            return false;
        }
     }
}
