using DataMapper.CustomMapperFramework;
using DtoLocalServerDALImplementation.DALImplementation.Common;
using DtoModel.DtoModels.Implementations.WindGeneratorDevice_History;
using DtoModel.DtoRequestObjectModels.Paging;
using DtoModel.DtoResponseObjectModels.WindGeneratorDevice_History;
using DtoServiceDAL.Interfaces.WindGeneratorDevice_History;
using RepoServiceDAL.Interfaces;
using RepositoryModel.RepoRequestObjectModels.Paging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DtoLocalServerDALImplementation.DALImplementation.WindGeneratorDevice_History_History
{
    public class DtoWindGeneratorDevice_HistoryDAL : DtoObjectBaseDAL, IDtoWindGeneratorDevice_HistoryDAL
    {
        public DtoWindGeneratorDevice_HistoryDAL(IRepositoryDAL inDbService)
        {
            dbService = inDbService;
        }


        #region Create
        public DtoWindGeneratorDevice_HistoryResponse Create(DtoWindGeneratorDevice_History inObject)
        {
            DtoWindGeneratorDevice_HistoryResponse toRet = new DtoWindGeneratorDevice_HistoryResponse();
            try
            {
                var dbObject = MyMapper.Convert(inObject); //mapper.Map<RepoEventDirectory>(inObject);
                var responseObj = dbService.GetWindGeneratorDevice_HistoryDAL().Create(dbObject);
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
                toRet.Message = string.Format("Error create WindGeneratorDevice_History.");
                toRet.MessageDescription = "Error details: " + Environment.NewLine + ex;
            }
            return toRet;
        }
        #endregion

        #region Delete
        public DtoWindGeneratorDevice_HistoryResponse Delete(DtoWindGeneratorDevice_History inObject)
        {
            return Delete(inObject?.Id ?? 0);
        }
        #endregion

        #region Delete
        public DtoWindGeneratorDevice_HistoryResponse Delete(long inObjectId)
        {
            DtoWindGeneratorDevice_HistoryResponse toRet = new DtoWindGeneratorDevice_HistoryResponse();

            try
            {
                var responseObj = dbService.GetWindGeneratorDevice_HistoryDAL().Delete(inObjectId);
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
                toRet.Message = string.Format("Error delete WindGeneratorDevice_History.");
                toRet.MessageDescription = "Error details: " + Environment.NewLine + ex;
            }
            return toRet;
        }
        #endregion

        #region DeleteWholeTableContent
        public DtoWindGeneratorDevice_HistoryResponse DeleteWholeTableContent()
        {
            var responseObj = dbService.GetWindGeneratorDevice_HistoryDAL().DeleteWholeTableContent();
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
        public DtoWindGeneratorDevice_HistoryResponse Get(long inId)
        {
            DtoWindGeneratorDevice_HistoryResponse toRet = new DtoWindGeneratorDevice_HistoryResponse();

            try
            {
                var dbObject = dbService.GetWindGeneratorDevice_HistoryDAL().Get(inId);
                toRet = MyMapper.Convert(dbObject); //mapper.Map<DtoEventDirectoryResponse>(dbObject);

            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = string.Format("Error get WindGeneratorDevice_History details.");
                toRet.MessageDescription = "Error details: " + Environment.NewLine + ex;
            }
            return toRet;
        }
        #endregion

        #region GetList
        public DtoWindGeneratorDevice_HistoryListResponse GetList(DtoPaging inPaging = null)
        {
            DtoWindGeneratorDevice_HistoryListResponse toRet = new DtoWindGeneratorDevice_HistoryListResponse();

            try
            {
                RepositoryPaging repopaging = MyMapper.Convert(inPaging); //mapper.Map<Repositorypaging>(inPaging);
                var dbList = dbService.GetWindGeneratorDevice_HistoryDAL().GetList(repopaging);//(RepositoryEventDirectoryListResponse)
                toRet = MyMapper.Convert(dbList); //mapper.Map<DtoEventDirectoryListResponse>(dbList);

            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = string.Format("Error query WindGeneratorDevice_History.");
                toRet.MessageDescription = "Error details: " + Environment.NewLine + ex;
            }
            return toRet;
        }
        #endregion

        #region Update
        public DtoWindGeneratorDevice_HistoryResponse Update(DtoWindGeneratorDevice_History inObject)
        {
            DtoWindGeneratorDevice_HistoryResponse toRet = new DtoWindGeneratorDevice_HistoryResponse();

            try
            {
                bool objExist = dbService.GetWindGeneratorDevice_HistoryDAL().ObjectExist(inObject?.Id ?? 0);
                if (objExist)
                {
                    var dbObject = MyMapper.Convert(inObject); //mapper.Map<RepoEventDirectory>(inObject);
                    var responseObj = dbService.GetWindGeneratorDevice_HistoryDAL().Update(dbObject);

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
                    toRet.Message = string.Format("Error update WindGeneratorDevice_History. WindGeneratorDevice_History with id: {0}, not found in database.", (inObject?.Id ?? 0));
                }
            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = string.Format("Error update WindGeneratorDevice_History.");
                toRet.MessageDescription = "Error details: " + Environment.NewLine + ex;
            }

            return toRet;
        }
        #endregion

        #region Soft Delete
        public DtoWindGeneratorDevice_HistoryResponse SoftDelete(DtoWindGeneratorDevice_History inObject)
        {
            return this.SoftDelete(inObject?.Id ?? 0);
        }

        public DtoWindGeneratorDevice_HistoryResponse SoftDelete(long inObjectId)
        {
            DtoWindGeneratorDevice_HistoryResponse toRet = new DtoWindGeneratorDevice_HistoryResponse();

            try
            {
                var responseObj = dbService.GetWindGeneratorDevice_HistoryDAL().SoftDelete(inObjectId);
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
                toRet.Message = string.Format("Error soft delete DtoWindGeneratorDevice_History.");
                toRet.MessageDescription = "Error soft details: " + Environment.NewLine + ex;
            }
            return toRet;
        }
        #endregion
    }
}
