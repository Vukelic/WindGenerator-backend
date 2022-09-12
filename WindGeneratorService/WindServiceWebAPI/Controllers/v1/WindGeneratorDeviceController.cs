using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using DtoModel.DtoModels.Implementations.WindGeneratorDevice;
using DtoModel.DtoRequestObjectModels.Paging;
using DtoModel.DtoResponseObjectModels.Profit;
using DtoModel.DtoResponseObjectModels.WindGeneratorDevice;
using DtoServiceDAL.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WindServiceWebAPI.Helpers.Common;
using WindServiceWebAPI.Models.CustomElectricity;

namespace WindServiceWebAPI.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class WindGeneratorDeviceController : ControllerBase
    {
        private ADtoDAL _dtoDAL { get; set; }
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        IConfigurationRoot config;
        string api_key;
        public WindGeneratorDeviceController(ADtoDAL dtoDAL, IAuthorizationService authorizationService, IHttpContextAccessor httpContextAccessor)
        {
            _dtoDAL = dtoDAL;
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
        }

        #region Post
        [Authorize]
        [HttpPost]
        [Route("Post")]
        //POST: /api/WindGeneratorDevice/Post
        public async Task<ActionResult<DtoWindGeneratorDeviceResponse>> Post([FromBody] DtoWindGeneratorDevice value)
        {
            DtoWindGeneratorDeviceResponse toRet = new DtoWindGeneratorDeviceResponse();
            try
            {
                if (value == null)
                {
                    throw new Exception("Object for created not exist.");
                }
                value.TimeCreated = DateTime.UtcNow;

                toRet = _dtoDAL?.GetWindGeneratorDeviceDAL()?.Create(value);               
            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = "Failed executing web api service.";
                toRet.MessageDescription = $"Error details: {ex}";
            }
            return toRet;
        }
        #endregion

        #region Delete
        [Authorize]
        [HttpDelete]
        [Route("Delete/{id}")]
        //POST: /api/WindGeneratorDevice/Delete
        public async Task<ActionResult<DtoWindGeneratorDeviceResponse>> Delete(long id)
        {
            DtoWindGeneratorDeviceResponse toRet = new DtoWindGeneratorDeviceResponse();
            try
            {
                toRet = _dtoDAL?.GetWindGeneratorDeviceDAL()?.Delete(id);             
            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = "Failed executing web api service.";
                toRet.MessageDescription = $"Error details: {ex}";
            }
            return toRet;
        }
        #endregion

        //TODO:ADD Update basic info for wind generator device(without value-power)
        #region Put
        [Authorize]
        [HttpPut]
        [Route("Put/{id}")]
        //POST: /api/WindGeneratorDevice/Put/{id}
        public async Task<ActionResult<DtoWindGeneratorDeviceResponse>> Put(long id, [FromBody] DtoWindGeneratorDevice value)
        {
            DtoWindGeneratorDeviceResponse toRet = new DtoWindGeneratorDeviceResponse();
            try
            {
                if (value == null)
                {
                    throw new Exception("Object for update not exist.");
                }
                if (value.Id != id)
                {
                    value.Id = id;
                }
                value.TimeModified = DateTime.UtcNow;

                //  toRet = _dtoDAL?.GetWindGeneratorDeviceDAL()?.Update(value);
                toRet = _dtoDAL?.GetWindGeneratorDeviceDAL()?.UpdateBasicInfoGenerator(value);
                
            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = "Failed executing web api service.";
                toRet.MessageDescription = $"Error details: {ex}";
            }
            return toRet;
        }
        #endregion

        #region Get(string inPaggingJson)
        [Authorize]
        [HttpGet]
        [Route("Get")]
        //POST: /api/WindGeneratorDevice/Get
        public async Task<ActionResult<DtoWindGeneratorDeviceListResponse>> Get(string inPaggingJson) //([FromBody]Dtopaging inpaging)

        {
            DtoWindGeneratorDeviceListResponse toRet = new DtoWindGeneratorDeviceListResponse();
            try
            {
                DtoPaging inpaging = null;
                if (!string.IsNullOrWhiteSpace(inPaggingJson))
                {
                    try
                    {
                        inpaging = JsonConvert.DeserializeObject<DtoPaging>(inPaggingJson);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                toRet = _dtoDAL?.GetWindGeneratorDeviceDAL()?.GetList(inpaging);
            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = "Failed executing web api service.";
                toRet.MessageDescription = $"Error details: {ex}";
            }
            return toRet;
        }
        #endregion

        #region Get(long id)
        [Authorize]
        [HttpGet]
        [Route("Get/{id}")]
        //POST: /api/WindGeneratorDevice/Get/{id}
        public async Task<ActionResult<DtoWindGeneratorDeviceResponse>> Get(long id)
        {
            DtoWindGeneratorDeviceResponse toRet = new DtoWindGeneratorDeviceResponse();
            try
            {
                toRet = _dtoDAL?.GetWindGeneratorDeviceDAL()?.Get(id);               
            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = "Failed executing web api service.";
                toRet.MessageDescription = $"Error details: {ex}";
            }
            return toRet;
        }
        #endregion

        #region CalculateProfit
        //[Authorize]
        [HttpPost]
        [Route("CalculateProfit")]
        //POST: /api/WindGeneratorDevice/CalculateProfit
        public async Task<ActionResult<DtoProfitResponse>> CalculateProfit([FromBody] DtoWindGeneratorDevice value)
        {
            DtoProfitResponse toRet = new DtoProfitResponse();
            try
            {
                var allData = readFromXmlFile();
                var priceElectricity = 0.0;
                if(allData != null && allData.Count > 0)
                {
                    foreach (var item in allData)
                    {
                        if(item.Country == value.Country)
                        {
                            priceElectricity = Convert.ToDouble(item.Price);
                            break;
                        }
                        else
                        {
                            priceElectricity = 0.5;// / 100; //mWH
                        
                        }
                    }
                }
               
                //koliko vetro dana ima vetrnjaca
                //kolika je snaga vetrenjace u 2000 mega vata
                //i onda mnozivim snagu * sate *mesec * 10godina = dobijem koliko bi potrosio unazad 10 godiina
                //koliko vetrog proizvede i koliko bi bilo kad bi kupila
                // snagu * sate* 30 za mesec dana
                //proveri koliko kosta 
                //*1000 u mega kilowatima

                var typeResponse = _dtoDAL?.GetWindGeneratorTypeDAL()?.Get(value.ParentWindGeneratorTypeId);
                var globalPriceOfTurbine = 0;
                var powerOfTurbine = 0.0;
  
                if (typeResponse != null && typeResponse.Success)
                {
                    var type = typeResponse.Value;

                    globalPriceOfTurbine = type.BasePrice + type.InstallationCosts;
                    powerOfTurbine = Convert.ToDouble(type.PowerOfTurbines); //max power of turbines in W
                }

                GetConfig();

                ApiHelper.InitializeClient(api_key);

                HistoryModel historyObject = GetHistoryWeatherInformationForGenerator(value);
                double averageWindPowerForLastYear = 0.0;
                double averagePowerOfTurbine = 0.0;
                if (historyObject != null)
                {
                    if(historyObject.result != null && historyObject.result.Count > 0)
                    {
                        for (int i = 0; i < historyObject.result.Count; i++)
                        {
                            averageWindPowerForLastYear += historyObject.result[i].wind.mean;
                        }

                        averageWindPowerForLastYear = averageWindPowerForLastYear / historyObject.result.Count;
                    }
                }
                averagePowerOfTurbine = Calculate_WindPower(averageWindPowerForLastYear, powerOfTurbine);

                var yearConsumptionOfTurbines = (averagePowerOfTurbine * averageWindPowerForLastYear); //proizvodnja kW/h turbine za godinu dana 

                var epsConsumtion20Years = priceElectricity * yearConsumptionOfTurbines * 20; // for 20 years eps

                 //globalna cena turbine globalPriceOfTurbine
                var profit = epsConsumtion20Years - globalPriceOfTurbine;

                toRet.Success = true;
                toRet.Profit = profit;
                toRet.ProfitabillityIndex = (profit / (20*12));

            }
            catch (Exception ex)
            {
                toRet.Success = false;
            }
            return toRet;
        }
        #endregion


        public List<CustomElectricity> readFromXmlFile()
        {
            List<CustomElectricity> toRet = new List<CustomElectricity>();

            XmlDocument doc = new XmlDocument();

            var currentDirectory = Directory.GetCurrentDirectory();
            doc.Load($"{currentDirectory}\\allData.xml");


            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                var country = String.Concat(node.FirstChild.InnerText.Where(c => !Char.IsWhiteSpace(c)));
                var price = String.Concat(node.LastChild.InnerText.Where(c => !Char.IsWhiteSpace(c)));
                toRet.Add(new CustomElectricity() { Country = country, Price = price });
            }

            return toRet;
        }

        private HistoryModel GetHistoryWeatherInformationForGenerator(DtoWindGeneratorDevice newGenerator)
        {
            HistoryModel toRet = null;
            try
            {
                toRet = WeatherProcessor.LoadHistoryWeather(newGenerator.GeographicalLatitude.ToString(), newGenerator.GeographicalLongitude.ToString()).Result;
                if (toRet == null)
                {
                    Console.WriteLine($"Wheater API NOT WORKING");
                }
                else
                {
                    Console.WriteLine($"Wheater API  WORKING");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wheater API NOT  WORKING {ex.Message}");
            }
            return toRet;
        }
        public void GetConfig()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            config = builder.Build();
            if (config != null)
            {
                try
                {
                   
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

        private double Calculate_WindPower(double Wind_Speed, double power)
        {
            //m/s
            double toRet = 0;
            double p_normal = power;
            const double ro = 1.293;
            const double A = 80;

            if (Wind_Speed < 3)
            {
                toRet = 0;
            }
            else if (Wind_Speed >= 3 && Wind_Speed <= 10)
            {
                var v3 = Math.Pow(Wind_Speed, 3);
                toRet = (0.5 * A * ro * v3); //to W
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

            if (toRet > p_normal) {
                toRet = p_normal;
            }           

            toRet = Math.Round(toRet, 2);
            return toRet;
        }
    }
}
