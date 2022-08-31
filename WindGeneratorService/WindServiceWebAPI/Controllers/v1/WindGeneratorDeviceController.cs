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
using Newtonsoft.Json;
using WindService_WindowsService.Api;
using WindService_WindowsService.Models;
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
        [Authorize]
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
                            priceElectricity = Convert.ToDouble(item.Price) * 1000;// + 0.5; //* 10;// / 100; //mWH
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
                var windHoursPerWay = 0.0;
                if (typeResponse != null && typeResponse.Success)
                {
                    var type = typeResponse.Value;

                    globalPriceOfTurbine = type.BasePrice + type.InstallationCosts;
                    powerOfTurbine = Convert.ToDouble(type.PowerOfTurbines); //power of turbines in MW
                    windHoursPerWay = Convert.ToDouble(type.GeneratorPower); //per day
                }

                var dailyConsumptionOfTurbines = powerOfTurbine * windHoursPerWay; //potrosnja turbine po danu
               
                var epsConsumtionFor10Years = priceElectricity * dailyConsumptionOfTurbines * 365 * 20; // last ten years eps

                 //globalna cena turbine globalPriceOfTurbine
                var profit = epsConsumtionFor10Years - globalPriceOfTurbine;

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

      
    }
}
