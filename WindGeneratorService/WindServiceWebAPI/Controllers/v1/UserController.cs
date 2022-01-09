using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DtoModel.DtoModels.Implementations.DtoChangeUserPassword;
using DtoModel.DtoModels.Implementations.User;
using DtoModel.DtoRequestObjectModels.Paging;
using DtoModel.DtoResponseObjectModels.User;
using DtoServiceDAL.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WindServiceWebAPI.Interfaces;
using WindServiceWebAPI.Models.AppTokenSetting;
using WindServiceWebAPI.Models.Common;

namespace WindServiceWebAPI.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private ADtoDAL _dtoDAL { get; set; }
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _userId;
        private readonly ITokenManager _tokenManager;
        private readonly AppTokenSettings _tokenManagement;


        public UserController(IOptions<AppTokenSettings> tokenManagement, ADtoDAL dtoDAL, ITokenManager tokenManager, IAuthorizationService authorizationService, IHttpContextAccessor httpContextAccessor)
        {
            _dtoDAL = dtoDAL;
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
            _userId = httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(i => i.Type == "UserId")?.Value;
            _tokenManagement = tokenManagement.Value;
            _tokenManager = tokenManager;
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

        #region Login

        [HttpPost]
        [Route("Login")]
        //POST: /api/Account/Login
        public ActionResult<DtoUserResponse> Login([FromBody] DtoUser value)
        {
            DtoUserResponse toRet = new DtoUserResponse();
            try
            {
                if (value == null)
                {
                    throw new Exception("Object for login not exist.");
                }

                toRet = _dtoDAL?.GetUserDAL()?.AuthenticateAsync(value.UserName, value.Password, false, 0, 0, 0);

                if (toRet.Success)
                {
                    var user = toRet.Value;

                    if (user.Susspend == true)
                    {
                        toRet.Success = false;
                        toRet.Message = "Account is suspended.";
                        return Ok(toRet);
                    }

                    if (user == null)
                    {
                        toRet.Success = false;
                        toRet.Message = "Username and password do not match.";
                        return Ok(toRet);
                    }



                    var roleResp = _dtoDAL?.GetRoleDAL()?.Get(user.AssignRoleId.Value);

                    if (!roleResp.Success)
                    {
                        toRet.Success = false;
                        toRet.Message = "Not found role.";
                        return Ok(toRet);
                    }



                    var tokenString = GenerateJWTToken.RequestToken(toRet.Value, roleResp.Value, _tokenManagement.Secret, _tokenManagement.ExpiryTime);

                    toRet = _dtoDAL?.GetUserDAL()?.UpdateTokenAsync(toRet.Value.Id, tokenString);


                }
                else
                {
                    toRet.Success = false;
                    toRet.Message = "Username and password do not match.";
                    return Ok(toRet);
                }
            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = "Failed executing web api service.";
                toRet.MessageDescription = $"Error details: {ex}";
            }
            return Ok(toRet);
        }

        #endregion

        #region Register
        [HttpPost]
        [Route("Register")]
        public ActionResult<DtoUserResponse> Register([FromBody] DtoUser userDto)
        {
            DtoUserResponse toRet = new DtoUserResponse();
            try
            {
                if (userDto == null)
                {
                    toRet.Success = false;
                    toRet.Message = "User is null.";
                    return BadRequest(toRet);
                }

                var resp = _dtoDAL?.GetUserDAL()?.FindByUsername(userDto.UserName);

                if (resp.Success || resp.Value != null)
                {
                    toRet.Success = false;
                    toRet.Message = "Username is existed in db.";
                    return BadRequest(toRet);
                }

                userDto.TimeCreated = DateTime.UtcNow;

                byte[] passwordHash = null;
                byte[] passwordSalt = null;

               var response = _dtoDAL?.GetUserDAL()?.CreatePasswordHash(userDto.Password, out passwordHash, out passwordSalt);

                if (response.Success)
                {
                    userDto.PasswordHash = passwordHash;
                    userDto.PasswordSalt = passwordSalt;
                    userDto.AssignRoleId = Startup.userRoleId;
                }
               

                toRet = _dtoDAL?.GetUserDAL()?.Create(userDto);

                if (toRet.Success != true)
                {
                    toRet.Success = false;
                    toRet.Message = toRet.Message;
                    toRet.MessageDescription = toRet.MessageDescription;
                    return BadRequest(toRet);
                }

            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = "Failed executing web api service.";
                toRet.MessageDescription = $"Error details: {ex}";
            }
            return Ok(toRet); ;

        }
        #endregion


        #region ChangePassword
        [HttpPost]
        [Authorize]
        [Route("ChangePassword")]
        public ActionResult<DtoUserResponse> ChangePassword([FromBody] DtoChangeUserPassword userDto)
        {
            DtoUserResponse toRet = new DtoUserResponse();

            try
            {
                if (userDto == null)
                {
                    toRet.Success = false;
                    toRet.Message = "User is null.";
                    return BadRequest(toRet);
                }

                if (userDto.UserId.ToString() != _userId)
                {
                    toRet.Success = false;
                    toRet.Message = "User is not matched.";
                    return BadRequest(toRet);
                }

                var userResonse = _dtoDAL?.GetUserDAL()?.Get(userDto.UserId);

                if (userResonse.Success != true || userResonse.Value == null)
                {
                    toRet.Success = false;
                    toRet.Message = "User is not exists in db.";
                    return BadRequest(toRet);
                }

                var user = userResonse.Value;

                var respCheckIfOldPasswordCorrect = _dtoDAL?.GetUserDAL()?.CheckIfOldPasswordCorrect(userDto.UserId, userDto.OldPassword);
                if (respCheckIfOldPasswordCorrect.Success != true)
                {
                    toRet.Success = false;
                    toRet.Message = respCheckIfOldPasswordCorrect.Message;
                    return BadRequest(toRet);
                }

                var responseUpdatePassword = _dtoDAL?.GetUserDAL()?.UpdatePassword(userDto.UserId, userDto.NewPassword);
                if (responseUpdatePassword.Success != true)
                {
                    toRet.Success = false;
                    toRet.Message = responseUpdatePassword.Message;
                    return BadRequest(toRet);

                }

                var roleResp = _dtoDAL?.GetRoleDAL()?.Get(user.AssignRoleId.Value);

                if (!roleResp.Success)
                {
                    toRet.Success = false;
                    toRet.Message = "Not found role.";
                    return Ok(toRet);
                }


                #region Update token


                var tokenString = GenerateJWTToken.RequestToken(user, roleResp.Value, _tokenManagement.Secret, _tokenManagement.ExpiryTime);

                toRet = _dtoDAL?.GetUserDAL()?.UpdateTokenAsync(user.Id, tokenString);
                #endregion

            }
            catch (Exception ex)
            {

                toRet.Success = false;
                toRet.Message = "Failed executing web api service.";
                toRet.MessageDescription = $"Error details: {ex}";
            }

            return Ok(toRet);
        }

        #endregion

        #region Logout
        [HttpGet]
        [Authorize]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {

            try
            {
                var response = await _tokenManager.DeactivateCurrentAsync();

                if (!response) { return BadRequest("User is not match"); }
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return Ok();
        }
        #endregion

    }
}
