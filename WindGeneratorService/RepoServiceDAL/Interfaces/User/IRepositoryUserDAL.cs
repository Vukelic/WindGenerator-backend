using RepoServiceDAL.Interfaces.Common;
using RepositoryModel.RepoModels.Implementations.User;
using RepositoryModel.RepoResponseObjectModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoServiceDAL.Interfaces.User
{
    public interface IRepositoryUserDAL : IRepositoryObjectBaseDAL<long, RepoUser>
    {
        RepositoryResponseBase<RepoUser> AuthenticateAsync(string username, string password, bool ProtectionActive, int NumberOfTrys, int LockTime, int TrackingInterval);
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPassword(string password);
        bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
        RepositoryResponseBase<RepoUser> FindByUsername(string username);
        RepositoryResponseBase<RepoUser> UpdateToken(RepoUser user);
        RepositoryResponseBase<RepoUser> UpdateTokenAsync(long id, string token);
        RepositoryResponseBase<RepoUser> IsTokenExists(string token);
        RepositoryResponseBase<RepoUser> RemoveToken(string token, string userId);
        RepositoryResponseBase<RepoUser> UpdateBasicInfoUser(RepoUser user);
        RepositoryResponseBase<RepoUser> LogOutUser(long userId);
    }
}
