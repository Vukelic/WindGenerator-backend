using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DtoModel.DtoModels.Implementations.User;
using DtoModel.DtoRequestObjectModels.Paging;
using DtoModel.DtoResponseObjectModels.User;
using DtoServiceDAL.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WindServiceWebAPI.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private ADtoDAL _dtoDAL { get; set; }
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UserController(ADtoDAL dtoDAL, IAuthorizationService authorizationService, IHttpContextAccessor httpContextAccessor)
        {
            _dtoDAL = dtoDAL;
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
        }

        #region Post
        // [Authorize]
        [HttpPost]
        [Route("Post")]
        //POST: /api/User/Post
        public async Task<ActionResult<DtoUserResponse>> Post([FromBody] DtoUser value)
        {
            DtoUserResponse toRet = new DtoUserResponse();
            try
            {
                if (value == null)
                {
                    throw new Exception("Object for created not exist.");
                }
                value.TimeCreated = DateTime.UtcNow;

                toRet = _dtoDAL?.GetUserDAL()?.Create(value);

                //#region AuthorizeAsync
                //TS_EntityResourseRequirementList allReq = new TS_EntityResourseRequirementList(
                // new TS_EntityResourseRequirement(EEntityType.User.ToString(), EEntityAction.CREATE.ToString(), 0, null, EEUserClaimConfigurationType.Advanced),
                // new TS_EntityResourseRequirement(EEntityType.ConfigurationSettings.ToString(), EEntityAction.CREATE.ToString(), 0, null, EEUserClaimConfigurationType.Basic)
                // );

                //var authorizationResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, value, allReq);

                //if (authorizationResult.Succeeded)
                //{
                //    toRet = _dtoDAL?.GetUserDAL()?.Create(value);

                //}
                //else if (User.Identity.IsAuthenticated)
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
        //POST: /api/User/Delete
        public async Task<ActionResult<DtoUserResponse>> Delete(long id)
        {
            DtoUserResponse toRet = new DtoUserResponse();
            try
            {
                toRet = _dtoDAL?.GetUserDAL()?.Delete(id);
                //#region AuthorizeAsync
                //TS_EntityResourseRequirementList allReq = new TS_EntityResourseRequirementList(
                //new TS_EntityResourseRequirement(EEntityType.User.ToString(), EEntityAction.DELETE.ToString(), id, null, EEUserClaimConfigurationType.Advanced),
                //new TS_EntityResourseRequirement(EEntityType.ConfigurationSettings.ToString(), EEntityAction.DELETE.ToString(), id, null, EEUserClaimConfigurationType.Basic)
                //);

                //var authorizationResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, id, allReq);

                //if (authorizationResult.Succeeded)
                //{
                //    var toRetUser = _dtoDAL?.GetUserDAL()?.Get(id);

                //    if (toRetUser.Success && toRetUser.Value != null)
                //    {
                //        if (!string.IsNullOrEmpty(toRetUser.Value.SystemString))
                //        {
                //            if (toRetUser.Value.SystemString.Contains("{system-admin-User}"))
                //            {
                //                return new ForbidResult();
                //            }
                //        }
                //    }

                //    var response = _dtoDAL?.GetUserDAL()?.IsConnectedWithOtherEntities(id); // bool
                //    if (response == false)
                //    {
                //        toRet = _dtoDAL?.GetUserDAL()?.Delete(id);
                //    }
                //    else
                //    {
                //        var responseGetAll = _dtoDAL?.GetUserDAL()?.GetAllConnectedEntities(id); // DtoUserShortInfo[]
                //        if (responseGetAll != null && responseGetAll.Count != 0)
                //        {
                //            dynamic tmpToRet = new ExpandoObject();
                //            if (responseGetAll.Count > 100) { tmpToRet.Users = responseGetAll.GetRange(0, 100); }
                //            else
                //            {
                //                tmpToRet.Users = responseGetAll;
                //            }
                //            //  tmpToRet.Users = responseGetAll.GetRange(0,100); //DtoUserShortInfo[]
                //            tmpToRet.Name = toRetUser.Value.Name;
                //            tmpToRet.Id = toRetUser.Value.Id;
                //            tmpToRet.Count = responseGetAll.Count;
                //            toRet.FailedDetails = tmpToRet;
                //            toRet.Success = false;
                //        }
                //        else
                //        {
                //            // Error, no information on connected users. Unexpeted error
                //        }
                //    }

                //}
                //else if (User.Identity.IsAuthenticated)
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
        //POST: /api/User/Put/{id}
        public async Task<ActionResult<DtoUserResponse>> Put(long id, [FromBody] DtoUser value)
        {
            DtoUserResponse toRet = new DtoUserResponse();
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

                toRet = _dtoDAL?.GetUserDAL()?.Update(value);

                // #region AuthorizeAsync
                // TS_EntityResourseRequirementList allReq = new TS_EntityResourseRequirementList(
                //new TS_EntityResourseRequirement(EEntityType.User.ToString(), EEntityAction.UPDATE.ToString(), id, null, EEUserClaimConfigurationType.Advanced),
                //new TS_EntityResourseRequirement(EEntityType.ConfigurationSettings.ToString(), EEntityAction.UPDATE.ToString(), id, null, EEUserClaimConfigurationType.Basic)
                //);

                // var authorizationResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, value, allReq);

                // if (authorizationResult.Succeeded)
                // {
                //     if (!string.IsNullOrEmpty(value.SystemString))
                //     {
                //         if (value.SystemString.Contains("{system-admin-User}"))
                //         {
                //             return new ForbidResult();
                //         }
                //     }

                //     toRet = _dtoDAL?.GetUserDAL()?.Update(value);

                //     #region Logout user after edit User
                //     if (toRet.Success && toRet.Value != null)
                //     {
                //         DtoPaging dtoPaging = new DtoPaging();
                //         dtoPaging.filters.Add("AssignUserId", $"{toRet.Value.Id}");
                //         dtoPaging.filtersType.Add("AssignUserId", "eq");
                //         var usersResponse = _dtoDAL.GetUserDAL().GetList(dtoPaging);

                //         if (usersResponse.Value != null && usersResponse.Success)
                //         {
                //             foreach (var user in usersResponse.Value)
                //             {
                //                 // user.UserToken = null;
                //                 var response = _dtoDAL.GetUserDAL()?.LogoutUser(user.Id);

                //                 if (!response.Success || response.Value == null)
                //                 {
                //                     toRet.Success = false;
                //                     toRet.Message = "Can not logout user.";
                //                     return toRet;
                //                 }
                //             }
                //         }

                //     }
                //     #endregion

                // }
                // else if (User.Identity.IsAuthenticated)
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
        //POST: /api/User/Get
        public async Task<ActionResult<DtoUserListResponse>> Get(string inPaggingJson) //([FromBody]Dtopaging inpaging)

        {
            DtoUserListResponse toRet = new DtoUserListResponse();
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
                toRet = _dtoDAL?.GetUserDAL()?.GetList(inpaging);

                //  #region AuthorizeAsync
                //  TS_EntityResourseRequirementList allReq = new TS_EntityResourseRequirementList(
                //new TS_EntityResourseRequirement(EEntityType.User.ToString(), EEntityAction.READ.ToString(), 0, null, EEUserClaimConfigurationType.Advanced),
                //new TS_EntityResourseRequirement(EEntityType.ConfigurationSettings.ToString(), EEntityAction.READ.ToString(), 0, null, EEUserClaimConfigurationType.Basic)
                //);

                //  var authorizationResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, inpaging, allReq);

                //  if (authorizationResult.Succeeded)
                //  {
                //      toRet = _dtoDAL?.GetUserDAL()?.GetList(inpaging);


                //  }
                //  else if (User.Identity.IsAuthenticated)
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
        //POST: /api/User/Get/{id}
        public async Task<ActionResult<DtoUserResponse>> Get(long id)
        {
            DtoUserResponse toRet = new DtoUserResponse();
            try
            {
                toRet = _dtoDAL?.GetUserDAL()?.Get(id);
                // #region AuthorizeAsync
                // TS_EntityResourseRequirementList allReq = new TS_EntityResourseRequirementList(
                //new TS_EntityResourseRequirement(EEntityType.User.ToString(), EEntityAction.READ.ToString(), id, null, EEUserClaimConfigurationType.Advanced),
                //new TS_EntityResourseRequirement(EEntityType.ConfigurationSettings.ToString(), EEntityAction.READ.ToString(), id, null, EEUserClaimConfigurationType.Basic)
                //);

                // var authorizationResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, id, allReq);

                // if (authorizationResult.Succeeded)
                // {
                //     toRet = _dtoDAL?.GetUserDAL()?.Get(id);

                // }
                // else if (User.Identity.IsAuthenticated)
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
