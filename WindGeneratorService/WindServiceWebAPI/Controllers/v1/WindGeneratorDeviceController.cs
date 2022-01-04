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
        // [Authorize]
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

                //#region AuthorizeAsync
                //TS_EntityResourseRequirementList allReq = new TS_EntityResourseRequirementList(
                // new TS_EntityResourseRequirement(EEntityType.WindGeneratorDevice.ToString(), EEntityAction.CREATE.ToString(), 0, null, EEWindGeneratorDeviceClaimConfigurationType.Advanced),
                // new TS_EntityResourseRequirement(EEntityType.ConfigurationSettings.ToString(), EEntityAction.CREATE.ToString(), 0, null, EEWindGeneratorDeviceClaimConfigurationType.Basic)
                // );

                //var authorizationResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.WindGeneratorDevice, value, allReq);

                //if (authorizationResult.Succeeded)
                //{
                //    toRet = _dtoDAL?.GetWindGeneratorDeviceDAL()?.Create(value);

                //}
                //else if (WindGeneratorDevice.Identity.IsAuthenticated)
                //{
                //    return new ForbidResult();
                //}
                //else
                //{
                //    return new ChallengeResult();
                //}
                //#endregion
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
        //  [Authorize]
        [HttpDelete]
        [Route("Delete/{id}")]
        //POST: /api/WindGeneratorDevice/Delete
        public async Task<ActionResult<DtoWindGeneratorDeviceResponse>> Delete(long id)
        {
            DtoWindGeneratorDeviceResponse toRet = new DtoWindGeneratorDeviceResponse();
            try
            {
                toRet = _dtoDAL?.GetWindGeneratorDeviceDAL()?.Delete(id);
                //#region AuthorizeAsync
                //TS_EntityResourseRequirementList allReq = new TS_EntityResourseRequirementList(
                //new TS_EntityResourseRequirement(EEntityType.WindGeneratorDevice.ToString(), EEntityAction.DELETE.ToString(), id, null, EEWindGeneratorDeviceClaimConfigurationType.Advanced),
                //new TS_EntityResourseRequirement(EEntityType.ConfigurationSettings.ToString(), EEntityAction.DELETE.ToString(), id, null, EEWindGeneratorDeviceClaimConfigurationType.Basic)
                //);

                //var authorizationResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.WindGeneratorDevice, id, allReq);

                //if (authorizationResult.Succeeded)
                //{
                //    var toRetWindGeneratorDevice = _dtoDAL?.GetWindGeneratorDeviceDAL()?.Get(id);

                //    if (toRetWindGeneratorDevice.Success && toRetWindGeneratorDevice.Value != null)
                //    {
                //        if (!string.IsNullOrEmpty(toRetWindGeneratorDevice.Value.SystemString))
                //        {
                //            if (toRetWindGeneratorDevice.Value.SystemString.Contains("{system-admin-WindGeneratorDevice}"))
                //            {
                //                return new ForbidResult();
                //            }
                //        }
                //    }

                //    var response = _dtoDAL?.GetWindGeneratorDeviceDAL()?.IsConnectedWithOtherEntities(id); // bool
                //    if (response == false)
                //    {
                //        toRet = _dtoDAL?.GetWindGeneratorDeviceDAL()?.Delete(id);
                //    }
                //    else
                //    {
                //        var responseGetAll = _dtoDAL?.GetWindGeneratorDeviceDAL()?.GetAllConnectedEntities(id); // DtoWindGeneratorDeviceShortInfo[]
                //        if (responseGetAll != null && responseGetAll.Count != 0)
                //        {
                //            dynamic tmpToRet = new ExpandoObject();
                //            if (responseGetAll.Count > 100) { tmpToRet.WindGeneratorDevices = responseGetAll.GetRange(0, 100); }
                //            else
                //            {
                //                tmpToRet.WindGeneratorDevices = responseGetAll;
                //            }
                //            //  tmpToRet.WindGeneratorDevices = responseGetAll.GetRange(0,100); //DtoWindGeneratorDeviceShortInfo[]
                //            tmpToRet.Name = toRetWindGeneratorDevice.Value.Name;
                //            tmpToRet.Id = toRetWindGeneratorDevice.Value.Id;
                //            tmpToRet.Count = responseGetAll.Count;
                //            toRet.FailedDetails = tmpToRet;
                //            toRet.Success = false;
                //        }
                //        else
                //        {
                //            // Error, no information on connected WindGeneratorDevices. Unexpeted error
                //        }
                //    }

                //}
                //else if (WindGeneratorDevice.Identity.IsAuthenticated)
                //{
                //    return new ForbidResult();
                //}
                //else
                //{
                //    return new ChallengeResult();
                //}
                //#endregion
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
        //  [Authorize]
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

                toRet = _dtoDAL?.GetWindGeneratorDeviceDAL()?.Update(value);

                // #region AuthorizeAsync
                // TS_EntityResourseRequirementList allReq = new TS_EntityResourseRequirementList(
                //new TS_EntityResourseRequirement(EEntityType.WindGeneratorDevice.ToString(), EEntityAction.UPDATE.ToString(), id, null, EEWindGeneratorDeviceClaimConfigurationType.Advanced),
                //new TS_EntityResourseRequirement(EEntityType.ConfigurationSettings.ToString(), EEntityAction.UPDATE.ToString(), id, null, EEWindGeneratorDeviceClaimConfigurationType.Basic)
                //);

                // var authorizationResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.WindGeneratorDevice, value, allReq);

                // if (authorizationResult.Succeeded)
                // {
                //     if (!string.IsNullOrEmpty(value.SystemString))
                //     {
                //         if (value.SystemString.Contains("{system-admin-WindGeneratorDevice}"))
                //         {
                //             return new ForbidResult();
                //         }
                //     }

                //     toRet = _dtoDAL?.GetWindGeneratorDeviceDAL()?.Update(value);

                //     #region Logout WindGeneratorDevice after edit WindGeneratorDevice
                //     if (toRet.Success && toRet.Value != null)
                //     {
                //         DtoPaging dtoPaging = new DtoPaging();
                //         dtoPaging.filters.Add("AssignWindGeneratorDeviceId", $"{toRet.Value.Id}");
                //         dtoPaging.filtersType.Add("AssignWindGeneratorDeviceId", "eq");
                //         var WindGeneratorDevicesResponse = _dtoDAL.GetWindGeneratorDeviceDAL().GetList(dtoPaging);

                //         if (WindGeneratorDevicesResponse.Value != null && WindGeneratorDevicesResponse.Success)
                //         {
                //             foreach (var WindGeneratorDevice in WindGeneratorDevicesResponse.Value)
                //             {
                //                 // WindGeneratorDevice.WindGeneratorDeviceToken = null;
                //                 var response = _dtoDAL.GetWindGeneratorDeviceDAL()?.LogoutWindGeneratorDevice(WindGeneratorDevice.Id);

                //                 if (!response.Success || response.Value == null)
                //                 {
                //                     toRet.Success = false;
                //                     toRet.Message = "Can not logout WindGeneratorDevice.";
                //                     return toRet;
                //                 }
                //             }
                //         }

                //     }
                //     #endregion

                // }
                // else if (WindGeneratorDevice.Identity.IsAuthenticated)
                // {
                //     return new ForbidResult();
                // }
                // else
                // {
                //     return new ChallengeResult();
                // }
                // #endregion
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
      //  [Authorize]
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

                //  #region AuthorizeAsync
                //  TS_EntityResourseRequirementList allReq = new TS_EntityResourseRequirementList(
                //new TS_EntityResourseRequirement(EEntityType.WindGeneratorDevice.ToString(), EEntityAction.READ.ToString(), 0, null, EEWindGeneratorDeviceClaimConfigurationType.Advanced),
                //new TS_EntityResourseRequirement(EEntityType.ConfigurationSettings.ToString(), EEntityAction.READ.ToString(), 0, null, EEWindGeneratorDeviceClaimConfigurationType.Basic)
                //);

                //  var authorizationResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.WindGeneratorDevice, inpaging, allReq);

                //  if (authorizationResult.Succeeded)
                //  {
                //      toRet = _dtoDAL?.GetWindGeneratorDeviceDAL()?.GetList(inpaging);


                //  }
                //  else if (WindGeneratorDevice.Identity.IsAuthenticated)
                //  {
                //      return new ForbidResult();
                //  }
                //  else
                //  {
                //      return new ChallengeResult();
                //  }
                //  #endregion
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
        //[Authorize]
        [HttpGet]
        [Route("Get/{id}")]
        //POST: /api/WindGeneratorDevice/Get/{id}
        public async Task<ActionResult<DtoWindGeneratorDeviceResponse>> Get(long id)
        {
            DtoWindGeneratorDeviceResponse toRet = new DtoWindGeneratorDeviceResponse();
            try
            {
                toRet = _dtoDAL?.GetWindGeneratorDeviceDAL()?.Get(id);
                // #region AuthorizeAsync
                // TS_EntityResourseRequirementList allReq = new TS_EntityResourseRequirementList(
                //new TS_EntityResourseRequirement(EEntityType.WindGeneratorDevice.ToString(), EEntityAction.READ.ToString(), id, null, EEWindGeneratorDeviceClaimConfigurationType.Advanced),
                //new TS_EntityResourseRequirement(EEntityType.ConfigurationSettings.ToString(), EEntityAction.READ.ToString(), id, null, EEWindGeneratorDeviceClaimConfigurationType.Basic)
                //);

                // var authorizationResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.WindGeneratorDevice, id, allReq);

                // if (authorizationResult.Succeeded)
                // {
                //     toRet = _dtoDAL?.GetWindGeneratorDeviceDAL()?.Get(id);

                // }
                // else if (WindGeneratorDevice.Identity.IsAuthenticated)
                // {
                //     return new ForbidResult();
                // }
                // else
                // {
                //     return new ChallengeResult();
                // }
                // #endregion
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
