using DataMapper.CustomMapperFramework;
using DtoLocalServerDALImplementation.DALImplementation.Common;
using DtoModel.DtoModels.Implementations.User;
using DtoModel.DtoRequestObjectModels.Paging;
using DtoModel.DtoResponseObjectModels.User;
using DtoServiceDAL.Interfaces.User;
using RepoServiceDAL.Interfaces;
using RepositoryModel.RepoRequestObjectModels.Paging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DtoLocalServerDALImplementation.DALImplementation.User
{
    public class DtoUserDAL : DtoObjectBaseDAL, IDtoUserDAL
    {
        public DtoUserDAL(IRepositoryDAL inDbService)
        {
            dbService = inDbService;
        }

        #region AuthenticateAsync
        public DtoUserResponse AuthenticateAsync(string username, string password, bool ProtectionActive, int NumberOfTrys, int LockTime, int TrackingInterval)
        {
            DtoUserResponse toRet = new DtoUserResponse();
            try
            {

                var responseObj = dbService.GetUserDAL().AuthenticateAsync(username, password, ProtectionActive, NumberOfTrys, LockTime, TrackingInterval);
                toRet = MyMapper.Convert(responseObj);
                if (responseObj.Success)
                {
                    toRet.Success = true;
                    toRet.Message = "Succes authenticate user.";
                    toRet.Value = MyMapper.Convert(responseObj.Value);

                    var saveResponse = dbService.SaveChanges();
                    if (!saveResponse.Success)
                    {
                        toRet.Success = false;
                        toRet.Message = saveResponse.Message;
                        toRet.MessageDescription = saveResponse.MessageDescription;
                        return toRet;
                    }
                }
                else
                {
                    toRet.Success = false;
                    toRet.Message = "Not authenticate user.";
                    toRet.Value = null;
                }

            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = string.Format("Error Authenticate User.");
                toRet.MessageDescription = "Error details: " + Environment.NewLine + ex;
            }
            return toRet;
        }
        #endregion

        #region Create
        public DtoUserResponse Create(DtoUser inObject)
        {
            DtoUserResponse toRet = new DtoUserResponse();
            try
            {
                var dbObject = MyMapper.Convert(inObject); //mapper.Map<RepoEventDirectory>(inObject);
                byte[] passwordHash;
                byte[] passwordSalt;
                dbService.GetUserDAL().CreatePasswordHash(inObject.Password, out passwordHash, out passwordSalt);
                dbObject.PasswordHash = passwordHash;
                dbObject.PasswordSalt = passwordSalt;
                var responseObj = dbService.GetUserDAL().Create(dbObject);
                toRet = MyMapper.Convert(responseObj); //mapper.Map<DtoEventDirectoryResponse>(responseObj);
                if (responseObj.Success)
                {
                    var saveResponse = dbService.SaveChanges();
                    if (!saveResponse.Success)
                    {
                        toRet.Success = false;
                        toRet.Message = saveResponse.Message;
                        toRet.MessageDescription = saveResponse.MessageDescription;
                    }
                    else
                    {
                        if (toRet != null && toRet.Value != null)
                        {
                            dbService.UpdateObjectPropertiesFromDb(dbObject);
                            toRet.Value.Id = dbObject.Id;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = string.Format("Error create User.");
                toRet.MessageDescription = "Error details: " + Environment.NewLine + ex;
            }
            return toRet;
        }
        #endregion

        #region CreatePasswordHash
        public DtoUserResponse CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {  //password kompleksnost duyina mala i velika slova
           //kreirati poseban metod za kontrolu logovanja koliko puta je neko pokusao i da se zakljuca nalog
            DtoUserResponse toRet = new DtoUserResponse();

            try
            {

                dbService.GetUserDAL().CreatePasswordHash(password, out passwordHash, out passwordSalt);

                toRet.Success = true;

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return toRet;
        }
        #endregion

        #region VerifyPassword
        public DtoUserResponse VerifyPassword(string password)
        {
            DtoUserResponse toRet = new DtoUserResponse();

            var responseObj = dbService.GetUserDAL().VerifyPassword(password);

            toRet.Success = responseObj;

            return toRet;
        }
        #endregion

        #region VerifyPasswordHash
        public DtoUserResponse VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            DtoUserResponse toRet = new DtoUserResponse();

            var responseObj = dbService.GetUserDAL().VerifyPasswordHash(password, storedHash, storedSalt);

            toRet.Success = responseObj;

            return toRet;
        }
        #endregion

        #region Delete(DtoUser inObject)
        public DtoUserResponse Delete(DtoUser inObject)
        {
            return Delete(inObject?.Id ?? 0);
        }
        #endregion

        #region Delete(long inObjectId)
        public DtoUserResponse Delete(long inObjectId)
        {
            DtoUserResponse toRet = new DtoUserResponse();

            try
            {
                var responseObj = dbService.GetUserDAL().Delete(inObjectId);
                toRet = MyMapper.Convert(responseObj); //mapper.Map<DtoEventDirectoryResponse>(responseObj);
                if (responseObj.Success)
                {
                    var saveResponse = dbService.SaveChanges();
                    if (!saveResponse.Success)
                    {
                        toRet.Success = false;
                        toRet.Message = saveResponse.Message;
                        toRet.MessageDescription = saveResponse.MessageDescription;
                    }
                }

            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = string.Format("Error delete User.");
                toRet.MessageDescription = "Error details: " + Environment.NewLine + ex;
            }
            return toRet;
        }
        #endregion

        #region  DeleteWholeTableContent
        public DtoUserResponse DeleteWholeTableContent()
        {
            var responseObj = dbService.GetUserDAL().DeleteWholeTableContent();
            var toRet = MyMapper.Convert(responseObj); //mapper.Map<DtoEventDirectoryResponse>(responseObj);
            if (responseObj.Success)
            {
                var saveResponse = dbService.SaveChanges();
                if (!saveResponse.Success)
                {
                    toRet.Success = false;
                    toRet.Message = saveResponse.Message;
                    toRet.MessageDescription = saveResponse.MessageDescription;
                }
            }
            return toRet;
        }
        #endregion

        #region Get
        public DtoUserResponse Get(long inId)
        {
            DtoUserResponse toRet = new DtoUserResponse();

            try
            {
                var dbObject = dbService.GetUserDAL().Get(inId);
                toRet = MyMapper.Convert(dbObject); //mapper.Map<DtoEventDirectoryResponse>(dbObject);

            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = string.Format("Error get User details.");
                toRet.MessageDescription = "Error details: " + Environment.NewLine + ex;
            }
            return toRet;
        }
        #endregion

        #region GetList
        public DtoUserListResponse GetList(DtoPaging inPaging = null)
        {
            DtoUserListResponse toRet = new DtoUserListResponse();

            try
            {
                RepositoryPaging repopaging = MyMapper.Convert(inPaging); //mapper.Map<Repositorypaging>(inPaging);
                var dbList = dbService.GetUserDAL().GetList(repopaging);//(RepositoryEventDirectoryListResponse)
                toRet = MyMapper.Convert(dbList); //mapper.Map<DtoEventDirectoryListResponse>(dbList);

            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = string.Format("Error query User.");
                toRet.MessageDescription = "Error details: " + Environment.NewLine + ex;
            }
            return toRet;
        }
        #endregion

        #region GetUsersWithRole
        public DtoUserListResponse GetUsersWithRole(DtoPaging inPaging = null)
        {
            DtoUserListResponse toRet = new DtoUserListResponse();

            try
            {
                RepositoryPaging repopaging = MyMapper.Convert(inPaging); //mapper.Map<Repositorypaging>(inPaging);
                var dbList = dbService.GetUserDAL().GetList(repopaging);//(RepositoryEventDirectoryListResponse)
                toRet = MyMapper.ConvertListUsersWithRole(dbList); //mapper.Map<DtoEventDirectoryListResponse>(dbList);

            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = string.Format("Error query User.");
                toRet.MessageDescription = "Error details: " + Environment.NewLine + ex;
            }
            return toRet;
        }
        #endregion

        #region GetUserWithRole
        public DtoUserResponse GetUserWithRole(long userId)
        {
            DtoUserResponse toRet = new DtoUserResponse();

            try
            {

                var dbList = dbService.GetUserDAL().Get(userId);//(RepositoryEventDirectoryListResponse)
                toRet = MyMapper.ConvertUserWithRole(dbList); //mapper.Map<DtoEventDirectoryListResponse>(dbList);

            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = string.Format("Error query User.");
                toRet.MessageDescription = "Error details: " + Environment.NewLine + ex;
            }
            return toRet;
        }
        #endregion

        #region Update
        public DtoUserResponse Update(DtoUser inObject)
        {
            DtoUserResponse toRet = new DtoUserResponse();

            try
            {
                bool objExist = dbService.GetUserDAL().ObjectExist(inObject?.Id ?? 0);
                if (objExist)
                {
                    var dbObject = MyMapper.Convert(inObject); //mapper.Map<RepoEventDirectory>(inObject);
                    var responseObj = dbService.GetUserDAL().Update(dbObject);

                    toRet = MyMapper.Convert(responseObj); //mapper.Map<DtoEventDirectoryResponse>(responseObj);
                    if (responseObj.Success)
                    {
                        var saveResponse = dbService.SaveChanges();
                        if (!saveResponse.Success)
                        {
                            toRet.Success = false;
                            toRet.Message = saveResponse.Message;
                            toRet.MessageDescription = saveResponse.MessageDescription;
                        }
                    }

                }
                else
                {
                    toRet.Success = false;
                    toRet.Message = string.Format("Error update User. User with id: {0}, not found in database.", (inObject?.Id ?? 0));
                }
            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = string.Format("Error update User.");
                toRet.MessageDescription = "Error details: " + Environment.NewLine + ex;
            }

            return toRet;
        }
        #endregion

        #region FindByUsername
        public DtoUserResponse FindByUsername(string username)
        {
            DtoUserResponse toRet = new DtoUserResponse();

            try
            {
                var response = dbService.GetUserDAL().FindByUsername(username);
                if (response.Success)
                {
                    toRet = MyMapper.Convert(response); //mapper.Map<RepoEventDirectory>(inObject);                   
                }
            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = string.Format("Error find User.");
                toRet.MessageDescription = "Error details: " + Environment.NewLine + ex;
            }

            return toRet;
        }


        #endregion

        #region UpdateTokenAsync
        public DtoUserResponse UpdateTokenAsync(long id, string token)
        {
            DtoUserResponse toRet = new DtoUserResponse();

            try
            {
                var response = dbService.GetUserDAL().UpdateTokenAsync(id, token);
                if (response.Success)
                {
                    toRet = MyMapper.Convert(response); //mapper.Map<RepoEventDirectory>(inObject);
                    var saveResponse = dbService.SaveChanges();
                    if (!saveResponse.Success)
                    {
                        toRet.Success = false;
                        toRet.Message = saveResponse.Message;
                        toRet.MessageDescription = saveResponse.MessageDescription;
                    }
                }
                else
                {
                    toRet.Success = false;
                    toRet.Message = string.Format("Error find user with id. User with id: {0}, not found in database.", (id));
                }
            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = string.Format("Error updateTokenAsync User.");
                toRet.MessageDescription = "Error details: " + Environment.NewLine + ex;
            }

            return toRet;
        }

        #endregion

        #region IsTokenExists
        public DtoUserResponse IsTokenExists(string token)
        {
            DtoUserResponse toRet = new DtoUserResponse();

            try
            {
                var response = dbService.GetUserDAL().IsTokenExists(token);
                if (response.Success)
                {
                    toRet = MyMapper.Convert(response); //mapper.Map<RepoEventDirectory>(inObject); 
                }
            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = string.Format("Error find Token.");
                toRet.MessageDescription = "Error details: " + Environment.NewLine + ex;
            }

            return toRet;
        }

        #endregion

        #region RemoveToken
        public DtoUserResponse RemoveToken(string token, string userId)
        {
            DtoUserResponse toRet = new DtoUserResponse();

            try
            {

                var response = dbService.GetUserDAL().RemoveToken(token, userId);
                if (response.Success)
                {
                    toRet = MyMapper.Convert(response); //mapper.Map<RepoEventDirectory>(inObject);
                    var saveResponse = dbService.SaveChanges();
                    if (!saveResponse.Success)
                    {
                        toRet.Success = false;
                        toRet.Message = saveResponse.Message;
                        toRet.MessageDescription = saveResponse.MessageDescription;
                        toRet.Value = null;
                    }

                }
                else
                {
                    toRet.Success = false;
                    toRet.Message = string.Format("Error find user with token. User with token: {0}, not found in database.", (token));
                }
            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = string.Format("Error find Token.");
                toRet.MessageDescription = "Error details: " + Environment.NewLine + ex;
            }

            return toRet;
        }

        #endregion

        #region UpdatePassword
        public DtoUserResponse UpdatePassword(long userId, string password)
        {
            DtoUserResponse toRet = new DtoUserResponse();

            var userResponse = dbService.GetUserDAL().Get(userId);
            if (userResponse.Success == false)
            {
                toRet.Success = false;
                toRet.Message = userResponse.Message;
                toRet.MessageDescription = userResponse.MessageDescription;
                toRet.Value = null;
                return toRet;
            }
            var user = userResponse.Value;
            if (user != null && !String.IsNullOrEmpty(password))
            {
                byte[] passwordHash;
                byte[] passwordSalt;
                var response = CreatePasswordHash(password, out passwordHash, out passwordSalt);

                if (response.Success != true)
                {
                    toRet.Success = false;
                    toRet.Message = response.Message;
                    toRet.MessageDescription = response.MessageDescription;
                    toRet.Value = null;
                    return toRet;
                }

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                try
                {
                    var responseObj = dbService.GetUserDAL().Update(user);

                    toRet = MyMapper.Convert(responseObj); //mapper.Map<DtoEventDirectoryResponse>(responseObj);
                    if (responseObj.Success)
                    {
                        var saveResponse = dbService.SaveChanges();
                        if (!saveResponse.Success)
                        {
                            toRet.Success = false;
                            toRet.Message = saveResponse.Message;
                            toRet.MessageDescription = saveResponse.MessageDescription;
                            return toRet;
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }

            return toRet;
        }


        #endregion

        #region ConfirmedEmail
        public DtoUserResponse ConfirmedEmail(long userId)
        {
            DtoUserResponse toRet = new DtoUserResponse();

            var userResponse = dbService.GetUserDAL().Get(userId);
            if (userResponse.Success == false)
            {
                toRet.Success = false;
                toRet.Message = userResponse.Message;
                toRet.MessageDescription = userResponse.MessageDescription;
                toRet.Value = null;
                return toRet;
            }
            var user = userResponse.Value;
            user.EmailConfirmed = true;

            try
            {
                var responseObj = dbService.GetUserDAL().Update(user);

                toRet = MyMapper.Convert(responseObj); //mapper.Map<DtoEventDirectoryResponse>(responseObj);
                if (responseObj.Success)
                {
                    var saveResponse = dbService.SaveChanges();
                    if (!saveResponse.Success)
                    {
                        toRet.Success = false;
                        toRet.Message = saveResponse.Message;
                        toRet.MessageDescription = saveResponse.MessageDescription;
                        return toRet;
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return toRet;
        }

        #endregion

        #region CheckIfOldPasswordCorrect
        public DtoUserResponse CheckIfOldPasswordCorrect(long userId, string oldPassword)
        {
            DtoUserResponse toRet = new DtoUserResponse();

            try
            {
                var userResponse = dbService.GetUserDAL().Get(userId);
                if (userResponse.Success == false)
                {
                    toRet.Success = false;
                    toRet.Message = userResponse.Message;
                    toRet.MessageDescription = userResponse.MessageDescription;
                    toRet.Value = null;
                    return toRet;
                }
                var user = userResponse.Value;

                toRet = VerifyPasswordHash(oldPassword, user.PasswordHash, user.PasswordSalt);

                if (toRet.Success != true)
                {
                    toRet.Success = false;
                    toRet.Message = "Password is incorrect.";
                    toRet.MessageDescription = "Password is incorrect.";
                    toRet.Value = null;
                    return toRet;
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }

            return toRet;
        }
        #endregion       

        #region UpdateBasicInfoUserWithoutPassword
        public DtoUserResponse UpdateBasicInfoUser(DtoUser user)
        {
            DtoUserResponse toRet = new DtoUserResponse();

            try
            {
                var repoUser = MyMapper.Convert(user);
                var userResponse = dbService.GetUserDAL().UpdateBasicInfoUser(repoUser);
                toRet = MyMapper.Convert(userResponse);

                if (userResponse.Success)
                {
                    var saveResponse = dbService.SaveChanges();
                    if (!saveResponse.Success)
                    {
                        toRet.Success = false;
                        toRet.Message = saveResponse.Message;
                        toRet.MessageDescription = saveResponse.MessageDescription;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return toRet;
        }

        #endregion

        #region LogoutUser
        public DtoUserResponse LogoutUser(long userId)
        {
            DtoUserResponse toRet = new DtoUserResponse();

            try
            {
                var userResponse = dbService.GetUserDAL().LogOutUser(userId);
                toRet = MyMapper.Convert(userResponse);
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return toRet;
        }
        #endregion

        #region Soft Delete
        public DtoUserResponse SoftDelete(DtoUser inObject)
        {
            return SoftDelete(inObject?.Id ?? 0);
        }

        public DtoUserResponse SoftDelete(long inObjectId)
        {
            DtoUserResponse toRet = new DtoUserResponse();

            try
            {
                var responseObj = dbService.GetUserDAL().SoftDelete(inObjectId);
                toRet = MyMapper.Convert(responseObj); //mapper.Map<DtoEventDirectoryResponse>(responseObj);
                if (responseObj.Success)
                {
                    var saveResponse = dbService.SaveChanges();
                    if (!saveResponse.Success)
                    {
                        toRet.Success = false;
                        toRet.Message = saveResponse.Message;
                        toRet.MessageDescription = saveResponse.MessageDescription;
                    }
                }

            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = string.Format("Error soft delete DtoUser.");
                toRet.MessageDescription = "Error soft details: " + Environment.NewLine + ex;
            }
            return toRet;
        }
        #endregion
    }
}
