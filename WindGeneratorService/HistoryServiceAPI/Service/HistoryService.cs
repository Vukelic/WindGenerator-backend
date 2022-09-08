using DtoModel.DtoModels.Implementations.WindGeneratorDevice;
using DtoModel.DtoModels.Implementations.WindGeneratorDevice_History;
using DtoServiceDAL.Abstractions;
using DtoServiceDAL.GlobalInstanceSelector;
using HistoryServiceAPI.Interfaces;
using HistoryServiceAPI.Models.Result;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WindService_WindowsService.Api;
using WindService_WindowsService.Models;

namespace HistoryServiceAPI.Service
{
    public class HistoryService : IHistoryService
    {
        private ADtoDAL _dtoDAL { get; set; }
        private readonly ILog _log;

        private List<DtoWindGeneratorDevice> WindGenerators = new List<DtoWindGeneratorDevice>();
        public HistoryService(ADtoDAL dtoDAL)
        {
            _dtoDAL = dtoDAL;
            _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }
        private List<DtoWindGeneratorDevice> GetWindGenerators()
        {
            ADtoDAL dtoDal = GlobalDtoDALInstanceSelector.GetDtoDALImplementation?.Invoke();

            var tmpGeneratorsList = dtoDal?.GetWindGeneratorDeviceDAL()?.GetList();
            _log.Info($"GetWindGenerators tmpWindGenerators count: {tmpGeneratorsList?.Value?.ToList()?.Count.ToString() ?? "null"} ");
            if (tmpGeneratorsList != null && tmpGeneratorsList.Success && tmpGeneratorsList.Value != null)
                return tmpGeneratorsList.Value.ToList();

            return null;
        }

        private double Calculate_WindPower(double Wind_Speed, string power = null)
        {
            double toRet = 0;
            double p_normal = Convert.ToDouble(power);
            const double ro = 1.293;
            const double A = 1;

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

        private WeatherModel GetWeatherInformationForGenerator(DtoWindGeneratorDevice newGenerator)
        {
            WeatherModel toRet = null;
            try
            {
                toRet = WeatherProcessor.LoadWeather(newGenerator.GeographicalLatitude.ToString(), newGenerator.GeographicalLongitude.ToString()).Result;
              
            }
            catch (Exception ex)
            {
                string dateStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            }
            return toRet;
        }

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

        private void GetGeneratorsInfo()
        {
            ADtoDAL dtoDal = GlobalDtoDALInstanceSelector.GetDtoDALImplementation?.Invoke();
            
                string dateStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
               _log.Info($"GetGeneratorsInfo, {dateStr}.\n");

                try
                {
                    var tmpWindGenerators = GetWindGenerators();
                   _log.Info($"GetGeneratorsInfo tmpWindGenerators count: {tmpWindGenerators?.Count.ToString() ?? "null"} ");
                    if (tmpWindGenerators != null)
                    {
                        foreach (var windGenerator in tmpWindGenerators)
                        {
                            var type = dtoDal?.GetWindGeneratorTypeDAL()?.Get(windGenerator.ParentWindGeneratorTypeId);
                            WeatherModel weatherModel = GetWeatherInformationForGenerator(windGenerator);

                            if (weatherModel != null)
                            {
                               _log.Info($"weatherModel not null GetGeneratorsInfo");
                                var wind_power = Calculate_WindPower(weatherModel.Current.Wind_Speed, type.Value.PowerOfTurbines);
                                windGenerator.ValueDec = (decimal)wind_power;
                                windGenerator.ValueStr = wind_power.ToString();
                                windGenerator.TimeCreated = DateTime.UtcNow;


                                var successfulCurrent = Update_WindGenerator_CurrentPower(windGenerator);
                               _log.Info($"update active generator GetGeneratorsInfo,{successfulCurrent}");
                                var successfulHistory = Add_HistoryWindGenerator(windGenerator);
                               _log.Info($"add to history generator GetGeneratorsInfo,{successfulHistory}");
                            }
                        }

                        lock (WindGenerators)
                        {
                           _log.Info($"lock");
                            WindGenerators = tmpWindGenerators; //for checking new generators. 
                        }
                    }
                }
                catch (Exception ex)
                {
                   _log.Info($"ERROR GetGeneratorsInfo {ex}");
                }

            

        }
        public async Task<AutoHistoryStartStopProcessResult> AutoStartHistoryAsync(double secondsOnTopOfMidnight = 15)
        {
            try
            {
                //TODO:Biljana
                return null;
            }
            catch (Exception ex)
            {
                string outAdditionalInfo = $"Exception rised during survey start process. Message: {ex.Message}. Details: /{ex}.";
                throw ex;
            }
        }

        public async Task<AutoHistoryStartStopProcessResult> AutoStopHistoryAsync(double secondsOnTopOfMidnight = 15)
        {
            try
            {
                //TODO:Biljana
                return null;
            }
            catch (Exception ex)
            {
                string outAdditionalInfo = $"Exception rised during survey stop process. Message: {ex.Message}. Details: /{ex}.";
                throw ex;
            }
        }
    }
}
