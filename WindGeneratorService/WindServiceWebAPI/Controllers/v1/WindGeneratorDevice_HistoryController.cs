using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DtoModel.DtoModels.Implementations.WindGeneratorDevice_History;
using DtoModel.DtoRequestObjectModels.Paging;
using DtoModel.DtoResponseObjectModels.WindGeneratorDevice_History;
using DtoServiceDAL.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WindServiceWebAPI.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class WindGeneratorDevice_HistoryController : ControllerBase
    {
        private ADtoDAL _dtoDAL { get; set; }
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public WindGeneratorDevice_HistoryController(ADtoDAL dtoDAL, IAuthorizationService authorizationService, IHttpContextAccessor httpContextAccessor)
        {
            _dtoDAL = dtoDAL;
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
        }

        #region Post
        [Authorize]
        [HttpPost]
        [Route("Post")]
        //POST: /api/WindGeneratorDevice_History/Post
        public async Task<ActionResult<DtoWindGeneratorDevice_HistoryResponse>> Post([FromBody] DtoWindGeneratorDevice_History value)
        {
            DtoWindGeneratorDevice_HistoryResponse toRet = new DtoWindGeneratorDevice_HistoryResponse();
            try
            {
                if (value == null)
                {
                    throw new Exception("Object for created not exist.");
                }
                value.TimeCreated = DateTime.UtcNow;

                toRet = _dtoDAL?.GetWindGeneratorDevice_HistoryDAL()?.Create(value);
           
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
        //POST: /api/WindGeneratorDevice_History/Delete
        public async Task<ActionResult<DtoWindGeneratorDevice_HistoryResponse>> Delete(long id)
        {
            DtoWindGeneratorDevice_HistoryResponse toRet = new DtoWindGeneratorDevice_HistoryResponse();
            try
            {
                toRet = _dtoDAL?.GetWindGeneratorDevice_HistoryDAL()?.Delete(id);              
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

        #region Put
        [Authorize]
        [HttpPut]
        [Route("Put/{id}")]
        //POST: /api/WindGeneratorDevice_History/Put/{id}
        public async Task<ActionResult<DtoWindGeneratorDevice_HistoryResponse>> Put(long id, [FromBody] DtoWindGeneratorDevice_History value)
        {
            DtoWindGeneratorDevice_HistoryResponse toRet = new DtoWindGeneratorDevice_HistoryResponse();
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

                toRet = _dtoDAL?.GetWindGeneratorDevice_HistoryDAL()?.Update(value);
               
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
        //POST: /api/WindGeneratorDevice_History/Get
        public async Task<ActionResult<DtoWindGeneratorDevice_HistoryListResponse>> Get(string inPaggingJson) //([FromBody]Dtopaging inpaging)

        {
            DtoWindGeneratorDevice_HistoryListResponse toRet = new DtoWindGeneratorDevice_HistoryListResponse();
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
                toRet = _dtoDAL?.GetWindGeneratorDevice_HistoryDAL()?.GetList(inpaging);
                if(toRet.Success && toRet.Value != null){
                    toRet.Value= toRet.Value.OrderBy(x => x.TimeCreated).ToList();
                }

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
        //POST: /api/WindGeneratorDevice_History/Get/{id}
        public async Task<ActionResult<DtoWindGeneratorDevice_HistoryResponse>> Get(long id)
        {
            DtoWindGeneratorDevice_HistoryResponse toRet = new DtoWindGeneratorDevice_HistoryResponse();
            try
            {
                toRet = _dtoDAL?.GetWindGeneratorDevice_HistoryDAL()?.Get(id);              
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
