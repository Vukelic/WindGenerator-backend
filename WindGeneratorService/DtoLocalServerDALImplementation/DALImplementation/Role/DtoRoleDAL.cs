using DataMapper.CustomMapperFramework;
using DtoLocalServerDALImplementation.DALImplementation.Common;
using DtoModel.DtoModels.Implementations.Role;
using DtoModel.DtoRequestObjectModels.Paging;
using DtoModel.DtoResponseObjectModels.Role;
using DtoServiceDAL.Interfaces.Role;
using RepoServiceDAL.Interfaces;
using RepositoryModel.RepoRequestObjectModels.Paging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DtoLocalServerDALImplementation.DALImplementation.Role
{
    public class DtoRoleDAL : DtoObjectBaseDAL, IDtoRoleDAL
    {
        public DtoRoleDAL(IRepositoryDAL inDbService)
        {
            dbService = inDbService;
        }
        

        #region Create
        public DtoRoleResponse Create(DtoRole inObject)
        {
            DtoRoleResponse toRet = new DtoRoleResponse();
            try
            {
                var dbObject = MyMapper.Convert(inObject); //mapper.Map<RepoEventDirectory>(inObject);
                var responseObj = dbService.GetRoleDAL().Create(dbObject);
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
                toRet.Message = string.Format("Error create Role.");
                toRet.MessageDescription = "Error details: " + Environment.NewLine + ex;
            }
            return toRet;
        }
        #endregion

        #region Delete
        public DtoRoleResponse Delete(DtoRole inObject)
        {
            return Delete(inObject?.Id ?? 0);
        }
        #endregion

        #region Delete
        public DtoRoleResponse Delete(long inObjectId)
        {
            DtoRoleResponse toRet = new DtoRoleResponse();

            try
            {
                var responseObj = dbService.GetRoleDAL().Delete(inObjectId);
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
                toRet.Message = string.Format("Error delete Role.");
                toRet.MessageDescription = "Error details: " + Environment.NewLine + ex;
            }
            return toRet;
        }
        #endregion

        #region DeleteWholeTableContent
        public DtoRoleResponse DeleteWholeTableContent()
        {
            var responseObj = dbService.GetRoleDAL().DeleteWholeTableContent();
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
        public DtoRoleResponse Get(long inId)
        {
            DtoRoleResponse toRet = new DtoRoleResponse();

            try
            {
                var dbObject = dbService.GetRoleDAL().Get(inId);
                toRet = MyMapper.Convert(dbObject); //mapper.Map<DtoEventDirectoryResponse>(dbObject);

            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = string.Format("Error get Role details.");
                toRet.MessageDescription = "Error details: " + Environment.NewLine + ex;
            }
            return toRet;
        }
        #endregion

        #region GetList
        public DtoRoleListResponse GetList(DtoPaging inPaging = null)
        {
            DtoRoleListResponse toRet = new DtoRoleListResponse();

            try
            {
                RepositoryPaging repopaging = MyMapper.Convert(inPaging); //mapper.Map<Repositorypaging>(inPaging);
                var dbList = dbService.GetRoleDAL().GetList(repopaging);//(RepositoryEventDirectoryListResponse)
                toRet = MyMapper.Convert(dbList); //mapper.Map<DtoEventDirectoryListResponse>(dbList);

            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = string.Format("Error query Role.");
                toRet.MessageDescription = "Error details: " + Environment.NewLine + ex;
            }
            return toRet;
        }
        #endregion

        #region Update
        public DtoRoleResponse Update(DtoRole inObject)
        {
            DtoRoleResponse toRet = new DtoRoleResponse();

            try
            {
                bool objExist = dbService.GetRoleDAL().ObjectExist(inObject?.Id ?? 0);
                if (objExist)
                {
                    var dbObject = MyMapper.Convert(inObject); //mapper.Map<RepoEventDirectory>(inObject);
                    var responseObj = dbService.GetRoleDAL().Update(dbObject);

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
                    toRet.Message = string.Format("Error update Role. Role with id: {0}, not found in database.", (inObject?.Id ?? 0));
                }
            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = string.Format("Error update Role.");
                toRet.MessageDescription = "Error details: " + Environment.NewLine + ex;
            }

            return toRet;
        }
        #endregion

        #region Soft Delete
        public DtoRoleResponse SoftDelete(DtoRole inObject)
        {
            return this.SoftDelete(inObject?.Id ?? 0);
        }

        public DtoRoleResponse SoftDelete(long inObjectId)
        {
            DtoRoleResponse toRet = new DtoRoleResponse();

            try
            {
                var responseObj = dbService.GetRoleDAL().SoftDelete(inObjectId);
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
                toRet.Message = string.Format("Error soft delete DtoRole.");
                toRet.MessageDescription = "Error soft details: " + Environment.NewLine + ex;
            }
            return toRet;
        }
        #endregion
    }
}
