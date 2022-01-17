using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DtoModel.DtoModels.Implementations.WindGeneratorDevice;
using DtoModel.DtoRequestObjectModels.Paging;
using DtoModel.DtoResponseObjectModels.WindGeneratorDevice;
using DtoServiceDAL.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

    }
}
