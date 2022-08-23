using DataMapper.CustomMapperFramework;
using DtoLocalServerDALImplementation.DALImplementation.Common;
using DtoModel.DtoModels.Implementations.WindGeneratorType;
using DtoModel.DtoRequestObjectModels.Paging;
using DtoModel.DtoResponseObjectModels.WindGeneratorType;
using DtoServiceDAL.Interfaces.WindGeneratorType;
using RepoServiceDAL.Interfaces;
using RepositoryModel.RepoRequestObjectModels.Paging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DtoLocalServerDALImplementation.DALImplementation.WindGeneratorType
{
    public class DtoWindGeneratorTypeDAL : DtoObjectBaseDAL, IDtoWindGeneratorTypeDAL
    {
        public DtoWindGeneratorTypeDAL(IRepositoryDAL inDbService)
        {
            dbService = inDbService;
        }


        #region Create
        public DtoWindGeneratorTypeResponse Create(DtoWindGeneratorType inObject)
        {
            DtoWindGeneratorTypeResponse toRet = new DtoWindGeneratorTypeResponse();
            try
            {
                var dbObject = MyMapper.Convert(inObject); //mapper.Map<RepoEventDirectory>(inObject);
                var responseObj = dbService.GetWindGeneratorTypeDAL().Create(dbObject);
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
                toRet.Message = string.Format("Error create WindGeneratorType.");
                toRet.MessageDescription = "Error details: " + Environment.NewLine + ex;
            }
            return toRet;
        }
        #endregion

        #region Delete
        public DtoWindGeneratorTypeResponse Delete(DtoWindGeneratorType inObject)
        {
            return Delete(inObject?.Id ?? 0);
        }
        #endregion

        #region Delete
        public DtoWindGeneratorTypeResponse Delete(long inObjectId)
        {
            DtoWindGeneratorTypeResponse toRet = new DtoWindGeneratorTypeResponse();

            try
            {
                var responseObj = dbService.GetWindGeneratorTypeDAL().Delete(inObjectId);
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
                toRet.Message = string.Format("Error delete WindGeneratorType.");
                toRet.MessageDescription = "Error details: " + Environment.NewLine + ex;
            }
            return toRet;
        }
        #endregion

        #region DeleteWholeTableContent
        public DtoWindGeneratorTypeResponse DeleteWholeTableContent()
        {
            var responseObj = dbService.GetWindGeneratorTypeDAL().DeleteWholeTableContent();
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
        public DtoWindGeneratorTypeResponse Get(long inId)
        {
            DtoWindGeneratorTypeResponse toRet = new DtoWindGeneratorTypeResponse();

            try
            {
                var dbObject = dbService.GetWindGeneratorTypeDAL().Get(inId);
                toRet = MyMapper.Convert(dbObject); //mapper.Map<DtoEventDirectoryResponse>(dbObject);

            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = string.Format("Error get WindGeneratorType details.");
                toRet.MessageDescription = "Error details: " + Environment.NewLine + ex;
            }
            return toRet;
        }
        #endregion

        #region GetList
        public DtoWindGeneratorTypeListResponse GetList(DtoPaging inPaging = null)
        {
            DtoWindGeneratorTypeListResponse toRet = new DtoWindGeneratorTypeListResponse();

            try
            {
                RepositoryPaging repopaging = MyMapper.Convert(inPaging); //mapper.Map<Repositorypaging>(inPaging);
                var dbList = dbService.GetWindGeneratorTypeDAL().GetList(repopaging);//(RepositoryEventDirectoryListResponse)
                toRet = MyMapper.Convert(dbList); //mapper.Map<DtoEventDirectoryListResponse>(dbList);

            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = string.Format("Error query WindGeneratorType.");
                toRet.MessageDescription = "Error details: " + Environment.NewLine + ex;
            }
            return toRet;
        }
        #endregion

        #region Update
        public DtoWindGeneratorTypeResponse Update(DtoWindGeneratorType inObject)
        {
            DtoWindGeneratorTypeResponse toRet = new DtoWindGeneratorTypeResponse();

            try
            {
                bool objExist = dbService.GetWindGeneratorTypeDAL().ObjectExist(inObject?.Id ?? 0);
                if (objExist)
                {
                    var dbObject = MyMapper.Convert(inObject); //mapper.Map<RepoEventDirectory>(inObject);
                    var responseObj = dbService.GetWindGeneratorTypeDAL().Update(dbObject);

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
                    toRet.Message = string.Format("Error update WindGeneratorType. WindGeneratorType with id: {0}, not found in database.", (inObject?.Id ?? 0));
                }
            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = string.Format("Error update WindGeneratorType.");
                toRet.MessageDescription = "Error details: " + Environment.NewLine + ex;
            }

            return toRet;
        }
        #endregion

        #region Soft Delete
        public DtoWindGeneratorTypeResponse SoftDelete(DtoWindGeneratorType inObject)
        {
            return this.SoftDelete(inObject?.Id ?? 0);
        }

        public DtoWindGeneratorTypeResponse SoftDelete(long inObjectId)
        {
            DtoWindGeneratorTypeResponse toRet = new DtoWindGeneratorTypeResponse();

            try
            {
                var responseObj = dbService.GetWindGeneratorTypeDAL().SoftDelete(inObjectId);
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
                toRet.Message = string.Format("Error soft delete DtoWindGeneratorType.");
                toRet.MessageDescription = "Error soft details: " + Environment.NewLine + ex;
            }
            return toRet;
        }
        #endregion
    }
}
