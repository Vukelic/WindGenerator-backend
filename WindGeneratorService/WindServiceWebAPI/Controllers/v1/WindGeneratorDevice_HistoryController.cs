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
        // [Authorize]
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

                //#region AuthorizeAsync
                //TS_EntityResourseRequirementList allReq = new TS_EntityResourseRequirementList(
                // new TS_EntityResourseRequirement(EEntityType.WindGeneratorDevice_History.ToString(), EEntityAction.CREATE.ToString(), 0, null, EEWindGeneratorDevice_HistoryClaimConfigurationType.Advanced),
                // new TS_EntityResourseRequirement(EEntityType.ConfigurationSettings.ToString(), EEntityAction.CREATE.ToString(), 0, null, EEWindGeneratorDevice_HistoryClaimConfigurationType.Basic)
                // );

                //var authorizationResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.WindGeneratorDevice_History, value, allReq);

                //if (authorizationResult.Succeeded)
                //{
                //    toRet = _dtoDAL?.GetWindGeneratorDevice_HistoryDAL()?.Create(value);

                //}
                //else if (WindGeneratorDevice_History.Identity.IsAuthenticated)
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
        //POST: /api/WindGeneratorDevice_History/Delete
        public async Task<ActionResult<DtoWindGeneratorDevice_HistoryResponse>> Delete(long id)
        {
            DtoWindGeneratorDevice_HistoryResponse toRet = new DtoWindGeneratorDevice_HistoryResponse();
            try
            {
                toRet = _dtoDAL?.GetWindGeneratorDevice_HistoryDAL()?.Delete(id);
                //#region AuthorizeAsync
                //TS_EntityResourseRequirementList allReq = new TS_EntityResourseRequirementList(
                //new TS_EntityResourseRequirement(EEntityType.WindGeneratorDevice_History.ToString(), EEntityAction.DELETE.ToString(), id, null, EEWindGeneratorDevice_HistoryClaimConfigurationType.Advanced),
                //new TS_EntityResourseRequirement(EEntityType.ConfigurationSettings.ToString(), EEntityAction.DELETE.ToString(), id, null, EEWindGeneratorDevice_HistoryClaimConfigurationType.Basic)
                //);

                //var authorizationResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.WindGeneratorDevice_History, id, allReq);

                //if (authorizationResult.Succeeded)
                //{
                //    var toRetWindGeneratorDevice_History = _dtoDAL?.GetWindGeneratorDevice_HistoryDAL()?.Get(id);

                //    if (toRetWindGeneratorDevice_History.Success && toRetWindGeneratorDevice_History.Value != null)
                //    {
                //        if (!string.IsNullOrEmpty(toRetWindGeneratorDevice_History.Value.SystemString))
                //        {
                //            if (toRetWindGeneratorDevice_History.Value.SystemString.Contains("{system-admin-WindGeneratorDevice_History}"))
                //            {
                //                return new ForbidResult();
                //            }
                //        }
                //    }

                //    var response = _dtoDAL?.GetWindGeneratorDevice_HistoryDAL()?.IsConnectedWithOtherEntities(id); // bool
                //    if (response == false)
                //    {
                //        toRet = _dtoDAL?.GetWindGeneratorDevice_HistoryDAL()?.Delete(id);
                //    }
                //    else
                //    {
                //        var responseGetAll = _dtoDAL?.GetWindGeneratorDevice_HistoryDAL()?.GetAllConnectedEntities(id); // DtoWindGeneratorDevice_HistoryShortInfo[]
                //        if (responseGetAll != null && responseGetAll.Count != 0)
                //        {
                //            dynamic tmpToRet = new ExpandoObject();
                //            if (responseGetAll.Count > 100) { tmpToRet.WindGeneratorDevice_Historys = responseGetAll.GetRange(0, 100); }
                //            else
                //            {
                //                tmpToRet.WindGeneratorDevice_Historys = responseGetAll;
                //            }
                //            //  tmpToRet.WindGeneratorDevice_Historys = responseGetAll.GetRange(0,100); //DtoWindGeneratorDevice_HistoryShortInfo[]
                //            tmpToRet.Name = toRetWindGeneratorDevice_History.Value.Name;
                //            tmpToRet.Id = toRetWindGeneratorDevice_History.Value.Id;
                //            tmpToRet.Count = responseGetAll.Count;
                //            toRet.FailedDetails = tmpToRet;
                //            toRet.Success = false;
                //        }
                //        else
                //        {
                //            // Error, no information on connected WindGeneratorDevice_Historys. Unexpeted error
                //        }
                //    }

                //}
                //else if (WindGeneratorDevice_History.Identity.IsAuthenticated)
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

                // #region AuthorizeAsync
                // TS_EntityResourseRequirementList allReq = new TS_EntityResourseRequirementList(
                //new TS_EntityResourseRequirement(EEntityType.WindGeneratorDevice_History.ToString(), EEntityAction.UPDATE.ToString(), id, null, EEWindGeneratorDevice_HistoryClaimConfigurationType.Advanced),
                //new TS_EntityResourseRequirement(EEntityType.ConfigurationSettings.ToString(), EEntityAction.UPDATE.ToString(), id, null, EEWindGeneratorDevice_HistoryClaimConfigurationType.Basic)
                //);

                // var authorizationResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.WindGeneratorDevice_History, value, allReq);

                // if (authorizationResult.Succeeded)
                // {
                //     if (!string.IsNullOrEmpty(value.SystemString))
                //     {
                //         if (value.SystemString.Contains("{system-admin-WindGeneratorDevice_History}"))
                //         {
                //             return new ForbidResult();
                //         }
                //     }

                //     toRet = _dtoDAL?.GetWindGeneratorDevice_HistoryDAL()?.Update(value);

                //     #region Logout WindGeneratorDevice_History after edit WindGeneratorDevice_History
                //     if (toRet.Success && toRet.Value != null)
                //     {
                //         DtoPaging dtoPaging = new DtoPaging();
                //         dtoPaging.filters.Add("AssignWindGeneratorDevice_HistoryId", $"{toRet.Value.Id}");
                //         dtoPaging.filtersType.Add("AssignWindGeneratorDevice_HistoryId", "eq");
                //         var WindGeneratorDevice_HistorysResponse = _dtoDAL.GetWindGeneratorDevice_HistoryDAL().GetList(dtoPaging);

                //         if (WindGeneratorDevice_HistorysResponse.Value != null && WindGeneratorDevice_HistorysResponse.Success)
                //         {
                //             foreach (var WindGeneratorDevice_History in WindGeneratorDevice_HistorysResponse.Value)
                //             {
                //                 // WindGeneratorDevice_History.WindGeneratorDevice_HistoryToken = null;
                //                 var response = _dtoDAL.GetWindGeneratorDevice_HistoryDAL()?.LogoutWindGeneratorDevice_History(WindGeneratorDevice_History.Id);

                //                 if (!response.Success || response.Value == null)
                //                 {
                //                     toRet.Success = false;
                //                     toRet.Message = "Can not logout WindGeneratorDevice_History.";
                //                     return toRet;
                //                 }
                //             }
                //         }

                //     }
                //     #endregion

                // }
                // else if (WindGeneratorDevice_History.Identity.IsAuthenticated)
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

                //  #region AuthorizeAsync
                //  TS_EntityResourseRequirementList allReq = new TS_EntityResourseRequirementList(
                //new TS_EntityResourseRequirement(EEntityType.WindGeneratorDevice_History.ToString(), EEntityAction.READ.ToString(), 0, null, EEWindGeneratorDevice_HistoryClaimConfigurationType.Advanced),
                //new TS_EntityResourseRequirement(EEntityType.ConfigurationSettings.ToString(), EEntityAction.READ.ToString(), 0, null, EEWindGeneratorDevice_HistoryClaimConfigurationType.Basic)
                //);

                //  var authorizationResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.WindGeneratorDevice_History, inpaging, allReq);

                //  if (authorizationResult.Succeeded)
                //  {
                //      toRet = _dtoDAL?.GetWindGeneratorDevice_HistoryDAL()?.GetList(inpaging);


                //  }
                //  else if (WindGeneratorDevice_History.Identity.IsAuthenticated)
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
                // #region AuthorizeAsync
                // TS_EntityResourseRequirementList allReq = new TS_EntityResourseRequirementList(
                //new TS_EntityResourseRequirement(EEntityType.WindGeneratorDevice_History.ToString(), EEntityAction.READ.ToString(), id, null, EEWindGeneratorDevice_HistoryClaimConfigurationType.Advanced),
                //new TS_EntityResourseRequirement(EEntityType.ConfigurationSettings.ToString(), EEntityAction.READ.ToString(), id, null, EEWindGeneratorDevice_HistoryClaimConfigurationType.Basic)
                //);

                // var authorizationResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.WindGeneratorDevice_History, id, allReq);

                // if (authorizationResult.Succeeded)
                // {
                //     toRet = _dtoDAL?.GetWindGeneratorDevice_HistoryDAL()?.Get(id);

                // }
                // else if (WindGeneratorDevice_History.Identity.IsAuthenticated)
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
