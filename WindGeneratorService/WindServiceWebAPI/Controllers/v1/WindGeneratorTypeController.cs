using DtoModel.DtoModels.Implementations.WindGeneratorType;
using DtoModel.DtoRequestObjectModels.Paging;
using DtoModel.DtoResponseObjectModels.WindGeneratorType;
using DtoServiceDAL.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WindServiceWebAPI.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class WindGeneratorTypeController : ControllerBase
    {
        private ADtoDAL _dtoDAL { get; set; }
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public WindGeneratorTypeController(ADtoDAL dtoDAL, IAuthorizationService authorizationService, IHttpContextAccessor httpContextAccessor)
        {
            _dtoDAL = dtoDAL;
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
        }

        #region Post
        [Authorize]
        [HttpPost]
        [Route("Post")]
        //POST: /api/WindGeneratorType/Post
        public async Task<ActionResult<DtoWindGeneratorTypeResponse>> Post([FromBody] DtoWindGeneratorType value)
        {
            DtoWindGeneratorTypeResponse toRet = new DtoWindGeneratorTypeResponse();
            try
            {
                if (value == null)
                {
                    throw new Exception("Object for created not exist.");
                }
                value.TimeCreated = DateTime.UtcNow;

                toRet = _dtoDAL?.GetWindGeneratorTypeDAL()?.Create(value);

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
        //POST: /api/WindGeneratorType/Delete
        public async Task<ActionResult<DtoWindGeneratorTypeResponse>> Delete(long id)
        {
            DtoWindGeneratorTypeResponse toRet = new DtoWindGeneratorTypeResponse();
            try
            {
                toRet = _dtoDAL?.GetWindGeneratorTypeDAL()?.Delete(id);
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
        //POST: /api/WindGeneratorType/Put/{id}
        public async Task<ActionResult<DtoWindGeneratorTypeResponse>> Put(long id, [FromBody] DtoWindGeneratorType value)
        {
            DtoWindGeneratorTypeResponse toRet = new DtoWindGeneratorTypeResponse();
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

                toRet = _dtoDAL?.GetWindGeneratorTypeDAL()?.Update(value);

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
        //POST: /api/WindGeneratorType/Get
        public async Task<ActionResult<DtoWindGeneratorTypeListResponse>> Get(string inPaggingJson) //([FromBody]Dtopaging inpaging)

        {
            DtoWindGeneratorTypeListResponse toRet = new DtoWindGeneratorTypeListResponse();
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
                toRet = _dtoDAL?.GetWindGeneratorTypeDAL()?.GetList(inpaging);
                if (toRet.Success && toRet.Value != null)
                {
                    toRet.Value = toRet.Value.OrderBy(x => x.TimeCreated).ToList();
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
        //POST: /api/WindGeneratorType/Get/{id}
        public async Task<ActionResult<DtoWindGeneratorTypeResponse>> Get(long id)
        {
            DtoWindGeneratorTypeResponse toRet = new DtoWindGeneratorTypeResponse();
            try
            {
                toRet = _dtoDAL?.GetWindGeneratorTypeDAL()?.Get(id);
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
