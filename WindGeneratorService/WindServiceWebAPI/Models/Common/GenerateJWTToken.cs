using DtoModel.DtoModels.Implementations.Role;
using DtoModel.DtoModels.Implementations.User;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WindServiceWebAPI.Models.Common
{
    public class GenerateJWTToken
    {
        public static string RequestToken(DtoUser user, DtoRole role, string secret, int expiration)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(secret);
                var SubjectClaims = new ClaimsIdentity();
                SubjectClaims.AddClaim(new Claim("UserId", user.Id.ToString()));
                SubjectClaims.AddClaim(new Claim("RoleId", user.AssignRoleId.ToString()));
                SubjectClaims.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                SubjectClaims.AddClaim(new Claim(ClaimTypes.Role, role.Name));


                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = SubjectClaims,
                    Expires = DateTime.Now.AddMinutes(expiration),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return tokenString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ClaimsPrincipal GetUserNameByToken(DtoUser user, string token, string secret)
        {
            try
            {
                var key = Encoding.ASCII.GetBytes(secret);
                var handler = new JwtSecurityTokenHandler();
                var validations = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(0)
                };
                var claims = handler.ValidateToken(token, validations, out var tokenSecure);
                return claims;//.Identity.Name;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
