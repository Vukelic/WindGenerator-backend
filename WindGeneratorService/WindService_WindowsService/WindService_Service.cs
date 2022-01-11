using DtoLocalServerDALImplementation;
using DtoModel.DtoModels.Implementations.WindGeneratorDevice;
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
                        //TODO: pozvati weather api za svaki wind generator
                    }

                    lock (WindGenerators)
                    {
                        WindGenerators = tmpWindGenerators; //for checking new generators. 
                    }
                }

                Thread.Sleep(getWindGeneratorsInfo_Thread_SleepTime);
            }

        }

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
                            //TODO: pozvati weather api za svaki novi generator...
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
            return WeatherProcessor.LoadWeather(newGenerator.GeographicalLatitude.ToString(), newGenerator.GeographicalLongitude.ToString()).Result;
        }
    }//[Class]
}//[Namespace]
