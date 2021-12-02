using DtoModel.DtoModels.Implementations.User;
using DtoModel.DtoRequestObjectModels.Paging;
using DtoModel.DtoResponseObjectModels.User;
using DtoServiceDAL.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DtoServiceDAL.Interfaces.User
{
   public interface IDtoUserDAL : IDtoObjectBaseDAL<long, DtoUser, DtoUserResponse, DtoUserListResponse>
    {
        DtoUserResponse AuthenticateAsync(string username, string password, bool ProtectionActive, int NumberOfTrys, int LockTime, int TrackingInterval);
        DtoUserResponse FindByUsername(string username);
        DtoUserResponse UpdateTokenAsync(long id, string token);
        DtoUserResponse IsTokenExists(string token);
        DtoUserResponse RemoveToken(string token, string userId);
        DtoUserResponse CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSal);
        DtoUserResponse UpdatePassword(long userId, string password);
        DtoUserResponse ConfirmedEmail(long userId);
        DtoUserResponse CheckIfOldPasswordCorrect(long userId, string oldPassword);
        DtoUserListResponse GetUsersWithRole(DtoPaging inPaging = null);
        DtoUserResponse GetUserWithRole(long userId);
        DtoUserResponse UpdateBasicInfoUser(DtoUser user);
        DtoUserResponse LogoutUser(long userId);
    }
}
