using DtoModel.DtoModels.Implementations.User;
using DtoModel.DtoResponseObjectModels.User;
using DtoServiceDAL.Abstractions;
using MailKit.Net.Imap;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using WindServiceAuthWebAPI.Models.AppTokenSettings;
using WindServiceAuthWebAPI.Models.Common;

namespace WindServiceAuthWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private ADtoDAL _dtoDAL { get; set; }
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _userId;
        private readonly AppTokenSettings _tokenManagement;


        public AccountController(IOptions<AppTokenSettings> tokenManagement, ADtoDAL dtoDAL,IAuthorizationService authorizationService, IHttpContextAccessor httpContextAccessor)
        {
            _dtoDAL = dtoDAL;
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
            _userId = httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(i => i.Type == "UserId")?.Value;
            _tokenManagement = tokenManagement.Value;
        }


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

                Random rnd = new Random();
                var pass = "" + rnd.Next();
                
               
                userDto.Password = pass;
                userDto.TimeCreated = DateTime.UtcNow;

                Task.Run(() =>
                {
                    var smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential("lazninalogtest1@gmail.com", "hyefgvicnapzdugu"),
                        EnableSsl = true,
                    };
                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress($"{userDto.UserName}"),
                        Subject = "Verify",
                        Body = $"<h1>Autentication</h1><p>Your password is: {pass}. Please, login.",
                        IsBodyHtml = true,
                    };
                    mailMessage.To.Add($"{userDto.UserName}");

                    smtpClient.Send(mailMessage);
                });
              
               

                byte[] passwordHash = null;
                byte[] passwordSalt = null;

                var response = _dtoDAL?.GetUserDAL()?.CreatePasswordHash(userDto.Password, out passwordHash, out passwordSalt);

                var responseRole = _dtoDAL?.GetRoleDAL()?.GetList();
                if (responseRole != null && responseRole.Success && responseRole.Value != null)
                {
                    var userRole = responseRole.Value.FirstOrDefault(o => o.SystemString == "{system-user-role}");

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
    }
}
