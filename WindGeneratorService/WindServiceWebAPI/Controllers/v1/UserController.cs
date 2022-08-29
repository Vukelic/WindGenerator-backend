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
        //POST: /api/User/Delete
        public async Task<ActionResult<DtoUserResponse>> Delete(long id)
        {
            DtoUserResponse toRet = new DtoUserResponse();
            try
            {
                toRet = _dtoDAL?.GetUserDAL()?.Delete(id);
               
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

                toRet = _dtoDAL?.GetUserDAL()?.UpdateBasicInfoUser(value);

               
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
                    return Ok(toRet);
                }

                var resp = _dtoDAL?.GetUserDAL()?.FindByUsername(userDto.UserName);

                if (resp.Success || resp.Value != null)
                {
                    toRet.Success = false;
                    toRet.Message = "Username is existed in db.";
                    return Ok(toRet);
                }

                userDto.TimeCreated = DateTime.UtcNow;

                byte[] passwordHash = null;
                byte[] passwordSalt = null;

               var response = _dtoDAL?.GetUserDAL()?.CreatePasswordHash(userDto.Password, out passwordHash, out passwordSalt);

                var responseRole = _dtoDAL?.GetRoleDAL()?.GetList();
                if(responseRole != null && responseRole.Success && responseRole.Value != null)
                {
                    var userRole = responseRole.Value.FirstOrDefault(o => o.SystemString == "{system-user-role}");

                    if(userRole != null)
                    {
                        if (response.Success)
                        {
                            userDto.PasswordHash = passwordHash;
                            userDto.PasswordSalt = passwordSalt;
                            userDto.AssignRoleId = userRole.Id;
                        }


                        toRet = _dtoDAL?.GetUserDAL()?.Create(userDto);

                        if (toRet.Success != true)
                        {
                            toRet.Success = false;
                            toRet.Message = toRet.Message;
                            toRet.MessageDescription = toRet.MessageDescription;
                            return Ok(toRet);
                        }
                    }
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

        #region RegisterAdmin
        [HttpPost]
        [Route("RegisterAdmin")]
        public ActionResult<DtoUserResponse> RegisterAdmin([FromBody] DtoUser userDto)
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

                var responseRole = _dtoDAL?.GetRoleDAL()?.GetList();
                if (responseRole != null && responseRole.Success && responseRole.Value != null)
                {
                    var userRole = responseRole.Value.FirstOrDefault(o => o.SystemString == "{system-admin-role}");

                    if (userRole != null)
                    {
                        if (response.Success)
                        {
                            userDto.PasswordHash = passwordHash;
                            userDto.PasswordSalt = passwordSalt;
                            userDto.AssignRoleId = userRole.Id;
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
                    return Ok(toRet);
                }

                var userResonse = _dtoDAL?.GetUserDAL()?.Get(userDto.UserId);

                if (userResonse.Success != true || userResonse.Value == null)
                {
                    toRet.Success = false;
                    toRet.Message = "User is not exists in db.";
                    return Ok(toRet);
                }

                var user = userResonse.Value;

                var respCheckIfOldPasswordCorrect = _dtoDAL?.GetUserDAL()?.CheckIfOldPasswordCorrect(userDto.UserId, userDto.OldPassword);
                if (respCheckIfOldPasswordCorrect.Success != true)
                {
                    toRet.Success = false;
                    toRet.Message = "Old password inccorect.";
                    return Ok(toRet);
                }

                toRet  = _dtoDAL?.GetUserDAL()?.UpdatePassword(userDto.UserId, userDto.NewPassword);
                if (toRet.Success != true)
                {
                    toRet.Success = false;
                    toRet.Message = toRet.Message;
                    return Ok(toRet);

                }

                var roleResp = _dtoDAL?.GetRoleDAL()?.Get(user.AssignRoleId.Value);

                if (!roleResp.Success)
                {
                    toRet.Success = false;
                    toRet.Message = "Not found role.";
                    return Ok(toRet);
                }


                #region Update token


                //var tokenString = GenerateJWTToken.RequestToken(user, roleResp.Value, _tokenManagement.Secret, _tokenManagement.ExpiryTime);

                //toRet = _dtoDAL?.GetUserDAL()?.UpdateTokenAsync(user.Id, tokenString);
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
