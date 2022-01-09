using DtoServiceDAL.Interfaces.User;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WindServiceWebAPI.Interfaces;
using WindServiceWebAPI.Models.AppTokenSetting;

namespace WindServiceWebAPI.Models.Common
{
    public class TokenManager : ITokenManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppTokenSettings _jwtOptions;
        private readonly IDtoUserDAL _dtoUserDal;
        string UserId;

        public TokenManager(IHttpContextAccessor httpContextAccessor, IOptions<AppTokenSettings> jwtOptions, IDtoUserDAL dtoUserDal)
        {
            _httpContextAccessor = httpContextAccessor;
            _jwtOptions = jwtOptions.Value;
            _dtoUserDal = dtoUserDal;
            var tUserName = httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(i => i.Type == ClaimTypes.Name)?.Value;
            var roleId = httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(i => i.Type == "ParentRoleId")?.Value;
            UserId = httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(i => i.Type == "UserId")?.Value;
        }
        public string GetCurrentToken()
            => GetCurrentAsync();

        public async Task<bool> IsCurrentActiveToken()
            => await IsActiveAsync(GetCurrentAsync());

        public async Task<bool> DeactivateCurrentAsync()
            => await DeactivateAsync(GetCurrentAsync());

        public async Task<bool> IsActiveAsync(string token)
        {
            if (!String.IsNullOrEmpty(token))
            {
                var response = _dtoUserDal.IsTokenExists(token);
                return response.Success;
            }
            else
            {
                return true;
            }
        }

        public async Task<bool> DeactivateAsync(string token)
        {
            var response = _dtoUserDal.RemoveToken(token, UserId);
            return response.Success;
        }


        private string GetCurrentAsync()
        {
            var authorizationHeader = _httpContextAccessor
                .HttpContext.Request.Headers["authorization"];

            return authorizationHeader == StringValues.Empty
                ? string.Empty
                : authorizationHeader.Single().Split(" ").Last();
        }
    }
}
