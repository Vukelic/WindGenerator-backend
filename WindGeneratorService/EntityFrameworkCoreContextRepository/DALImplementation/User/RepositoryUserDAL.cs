using CommonModels.Exceptions;
using EntityFrameworkCoreContextRepository.Context;
using EntityFrameworkCoreContextRepository.DALImplementation.Common;
using RepoServiceDAL.Interfaces.User;
using RepositoryModel.RepoModels.Implementations.User;
using RepositoryModel.RepoResponseObjectModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EntityFrameworkCoreContextRepository.DALImplementation.User
{
    public class RepositoryUserDAL : RepositoryEntityObjectBaseDAL<RepoUser>, IRepositoryUserDAL
    {
        public RepositoryUserDAL(WindServiceMainDbContext inDb) : base(inDb)
        {

        }

        #region AuthenticateAsync
        public RepositoryResponseBase<RepoUser> AuthenticateAsync(string username, string password, bool ProtectionActive, int NumberOfTrys, int LockTime, int TrackingInterval)
        {
            RepositoryResponseBase<RepoUser> toRet = new RepositoryResponseBase<RepoUser>();
            try
            {
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    toRet.Success = false;
                    toRet.Message = "Username or passworn cannot be empty or whitespace only string";
                    toRet.Value = null;
                    return toRet;
                }


                var response = FindByUsername(username);  //return user with roles

                // check if username exists
                if (response.Success != true || response.Value == null)
                {
                    toRet.Success = false;
                    toRet.Message = "User not existed in database.";
                    toRet.Value = null;
                    return toRet;
                }

                var user = response.Value;
                //if (user.EmailConfirmed == false)
                //    throw new UserAccountNotConfirmedException();

                //if (user.Active == false)
                //    throw new UserAccountNotActiveException();

                var success = VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt);

                if (ProtectionActive)
                {
                    //TimeSpan ts1 = new TimeSpan();
                    //ts1 = user.StartTrackingInterval == null ? DateTime.Now.AddMinutes(TrackingInterval) - DateTime.Now : user.StartTrackingInterval.Value.AddMinutes(TrackingInterval) - DateTime.Now;
                    var attept = user.FailedAttempt;
                    if (attept < NumberOfTrys && (!user.StartTrackingInterval.HasValue || DateTime.Now > user.StartTrackingInterval.Value.AddMinutes(TrackingInterval)))
                    {
                        user.StartTrackingInterval = DateTime.Now;
                        user.FailedAttempt = 0;
                        var respUpd = Update(user);

                        if (respUpd.Success != true || respUpd.Value == null)
                        {
                            toRet.Success = false;
                            toRet.Message = "Error with update user in database.";
                            toRet.Value = null;
                            return toRet;
                        }
                    }


                    attept++;

                    if (attept > NumberOfTrys && user.LastLoginTime.HasValue && DateTime.Now < user.LastLoginTime.Value.AddMinutes(LockTime))
                    {
                        //TimeSpan ts = user.LastLoginTime.Value.AddMinutes(LockTime) - DateTime.Now;
                        //var message = String.Format("lock: {0} minuta i {1} sekundi. Interval {2} minuta i {3} sekundi.", ts.Minutes, ts.Seconds, ts1.Minutes, ts1.Seconds);
                        throw new UserAccountLockException();
                    }

                    if (attept > NumberOfTrys && user.LastLoginTime.HasValue && DateTime.Now >= user.LastLoginTime.Value.AddMinutes(LockTime)) //kad istekne kazna da dozvolim naredne uspjesne pokusaje
                    {
                        user.FailedAttempt--;
                        var respUpd = Update(user);

                        if (respUpd.Success != true || respUpd.Value == null)
                        {
                            toRet.Success = false;
                            toRet.Message = "Error with update user in database.";
                            toRet.Value = null;
                            return toRet;
                        }
                    }

                    if (!success)
                    {
                        user.LastLoginTime = DateTime.Now; //or UTC
                        user.FailedAttempt++;
                        var respUpd = Update(user);

                        if (respUpd.Success != true || respUpd.Value == null)
                        {
                            toRet.Success = false;
                            toRet.Message = "Error with update user in database.";
                            toRet.Value = null;
                            return toRet;
                        }

                        toRet.Success = false;
                        toRet.Message = "Not succes Verify Password Hash.";
                        toRet.Value = null;
                        return toRet;
                    }
                    else
                    {
                        user.LastLoginTime = DateTime.Now;
                        var respUpd = Update(user);

                        if (respUpd.Success != true || respUpd.Value == null)
                        {
                            toRet.Success = false;
                            toRet.Message = "Error with update user in database.";
                            toRet.Value = null;
                            return toRet;
                        }

                    }
                }
                else
                {
                    if (!success)
                    {
                        toRet.Success = false;
                        toRet.Message = "Not succes Verify Password Hash.";
                        toRet.Value = null;
                        return toRet;
                    }

                }

                // authentication successful
                toRet.Success = true;
                toRet.Message = "Successful authenticate user.";
                toRet.Value = user;
                return toRet;
            }
            catch (UserAccountLockException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region FindByUsername
        public RepositoryResponseBase<RepoUser> FindByUsername(string username)
        {
            RepositoryResponseBase<RepoUser> toRet = new RepositoryResponseBase<RepoUser>();

            if (username != null && username != " ")
            {
                var foundedObj = db.Users.FirstOrDefault(o => o.UserName == username);
                if (foundedObj != null)
                {
                    toRet.Success = true;
                    toRet.Value = foundedObj;
                    toRet.Message = "Succesfully finded user.";
                }
                else
                {
                    toRet.Success = false;
                    toRet.Value = null;
                    toRet.Message = "Username not existed in database";
                }
            }
            else
            {
                toRet.Success = false;
                toRet.Value = null;
                toRet.Message = "Username cannot be empty or whitespace only string.";
            }

            return toRet;
        }

        #endregion

        #region CreatePasswordHash
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            RepositoryUserDAL.CreatePasswordHashStatic(password, out passwordHash, out passwordSalt);
        }

        public static void CreatePasswordHashStatic(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {  //password kompleksnost duyina mala i velika slova
            //kreirati poseban metod za kontrolu logovanja koliko puta je neko pokusao i da se zakljuca nalog
            if (password == null) throw new ArgumentNullException("password");
            if (!RepositoryUserDAL.VerifyPasswordStatic(password)) throw new ArgumentException("Value must have min 8 characters and max 30 it must contains 1 upper case and 1 number!", "password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        #endregion

        #region VerifyPassword
        public bool VerifyPassword(string password)
        {
            return RepositoryUserDAL.VerifyPasswordStatic(password);
        }

        public static bool VerifyPasswordStatic(string password)
        {
            Regex rex = new Regex(@"^(?=.*[A-Z])(?=.*\d).{8,30}$");
            return rex.IsMatch(password);
        }
        #endregion

        #region VerifyPasswordHash
        public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (!VerifyPassword(password)) throw new ArgumentException("Value must have min 8 characters and max 30 it must contains 1 upper case and 1 number!", "password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
        #endregion

        #region UpdateToken
        public RepositoryResponseBase<RepoUser> UpdateToken(RepoUser user)
        {
            RepositoryResponseBase<RepoUser> toRet = new RepositoryResponseBase<RepoUser>();

            try
            {
                var response = db.Users.Update(user);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return new RepositoryResponseBase<RepoUser>() { Success = true, Message = "User updateToken successfull", Value = user };
        }
        #endregion

        #region UpdateTokenAsync
        public RepositoryResponseBase<RepoUser> UpdateTokenAsync(long id, string token)
        {
            var existingUser = db.Users.FirstOrDefault(o => o.Id == id);

            if (existingUser == null)
                return new RepositoryResponseBase<RepoUser>() { Success = false, Message = "User not found", Value = null };

            existingUser.UserToken = token;

            try
            {
                var result = UpdateToken(existingUser);

                if (result.Success == false)
                {
                    return new RepositoryResponseBase<RepoUser>() { Success = false, Message = "User not found", Value = null };

                }
                return new RepositoryResponseBase<RepoUser>() { Success = true, Message = "User updateTokenAsync successfull", Value = result.Value };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region IsTokenExists
        public RepositoryResponseBase<RepoUser> IsTokenExists(string token)
        {
            var existingUser = db.Users.FirstOrDefault(o => o.UserToken == token);

            if (existingUser == null)
                return new RepositoryResponseBase<RepoUser>() { Success = false, Message = "User not found", Value = null };


            return new RepositoryResponseBase<RepoUser>() { Success = true, Message = "User successfull found.", Value = existingUser };
        }

        #endregion

        #region RemoveToken
        public RepositoryResponseBase<RepoUser> RemoveToken(string token, string userId)
        {
            var response = IsTokenExists(token);

            if (response.Success == false || response.Value == null)
                return new RepositoryResponseBase<RepoUser>() { Success = false, Message = "User not found", Value = null };

            if (response.Value.Id != Convert.ToInt32(userId))
                return new RepositoryResponseBase<RepoUser>() { Success = false, Message = "User is not matched", Value = null };

            var user = response.Value;
            user.UserToken = null;

            try
            {
                UpdateToken(user);
                return new RepositoryResponseBase<RepoUser>() { Success = true, Message = "Token successfull removed", Value = null };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region UpdateBasicInfoUserWithoutPassword
        public RepositoryResponseBase<RepoUser> UpdateBasicInfoUser(RepoUser user)
        {
            var existingUser = db.Users.FirstOrDefault(o => o.Id == user.Id);

            if (existingUser == null)
                return new RepositoryResponseBase<RepoUser>() { Success = false, Message = "User not found", Value = null };

            try
            {
                user.PasswordHash = existingUser.PasswordHash;
                user.PasswordSalt = existingUser.PasswordSalt;

                var result = Update(user);

                if (result.Success == false)
                {
                    return new RepositoryResponseBase<RepoUser>() { Success = false, Message = "User not found", Value = null };

                }
                return new RepositoryResponseBase<RepoUser>() { Success = true, Message = "User updateTokenAsync successfull", Value = result.Value };
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        #endregion

        #region LogOutUser
        public RepositoryResponseBase<RepoUser> LogOutUser(long userId)
        {
            var existingUser = db.Users.FirstOrDefault(o => o.Id == userId);

            if (existingUser == null)
                return new RepositoryResponseBase<RepoUser>() { Success = false, Message = "User not found", Value = null };

            try
            {

                existingUser.UserToken = null;
                var result = Update(existingUser);

                if (result.Success == false)
                {
                    return new RepositoryResponseBase<RepoUser>() { Success = false, Message = "User not found", Value = null };

                }
                return new RepositoryResponseBase<RepoUser>() { Success = true, Message = "User updateTokenAsync successfull", Value = result.Value };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
