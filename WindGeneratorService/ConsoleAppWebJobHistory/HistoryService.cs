using ConsoleAppWebJobHistory.Helpers;
using ConsoleAppWebJobHistory.Models;
using DtoLocalServerDALImplementation;
using DtoModel.DtoModels.Implementations.WindGeneratorDevice;
using DtoModel.DtoModels.Implementations.WindGeneratorDevice_History;
using DtoServiceDAL.Abstractions;
using DtoServiceDAL.GlobalInstanceSelector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleAppWebJobHistory
{
   public class HistoryService
    {
       // Thread getWindGeneratorsInfo_Thread;
       // TimeSpan getWindGeneratorsInfo_Thread_SleepTime = new TimeSpan(1, 0, 0, 0, 0);

        bool _stop = false;

        
        string connectionString;
        string api_key;

        public HistoryService()
        {
            #region apis
            connectionString = "Data Source=tcp:wind-service-database2.database.windows.net,1433;Initial Catalog=WindServiceWebAPI_db;User Id=tmp@wind-service-database2;Password=SuperAdmin!1";
        //    connectionString = "Server=DESKTOP-H4344E1\\SQLEXPRESS;Database=wind-service-database2;Trusted_Connection=True;MultipleActiveResultSets=true";
             api_key = "f2b96f1e4f4bdd3fecced2a1e49c7a71";
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

            OnStart();
      //      System.Threading.Thread.CurrentThread.Join();
        }

        private void OnStart()
        {

            Console.WriteLine("OnStart");
            StartProcesses();
        }
        private void OnStop()
        {
            string dateStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            StopProcesses();
        }


        //Start & Stop processes
        private void StopProcesses()
        {
            string dateStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            _stop = true;
        }

        private void StartProcesses()
        {
            _stop = false;
            Console.WriteLine("StartProcesses");
            //getWindGeneratorsInfo_Thread = new Thread(new ThreadStart(GetGeneratorsInfo));
            //getWindGeneratorsInfo_Thread.Start();
            GetGeneratorsInfo();
        }

        //Threads functions

        private void GetGeneratorsInfo()
        {
            ADtoDAL dtoDal = GlobalDtoDALInstanceSelector.GetDtoDALImplementation?.Invoke();
            while (!_stop)
            {

                string dateStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                try
                {
                    var tmpWindGenerators = GetWindGenerators();
               
                    if (tmpWindGenerators != null)
                    {
                        foreach (var windGenerator in tmpWindGenerators)
                        {
                           
                            if (windGenerator != null)
                            {
                                var successfulHistory = Add_HistoryWindGenerator(windGenerator);
                            }
                        }

                        
                    }
                }
                catch (Exception ex)
                {
                   
                }

               // Thread.Sleep(getWindGeneratorsInfo_Thread_SleepTime);
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
                Console.WriteLine($"ex: {ex.Message}");
            }
            return toRet;
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
    }
}
