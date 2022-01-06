using DataMapper.CustomMapperFramework;
using DtoLocalServerDALImplementation.DALImplementation.Common;
using DtoModel.DtoModels.Implementations.WindGeneratorDevice;
using DtoModel.DtoRequestObjectModels.Paging;
using DtoModel.DtoResponseObjectModels.WindGeneratorDevice;
using DtoServiceDAL.Interfaces.WindGeneratorDevice;
using RepoServiceDAL.Interfaces;
using RepositoryModel.RepoRequestObjectModels.Paging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DtoLocalServerDALImplementation.DALImplementation.WindGeneratorDevice
{
    public class DtoWindGeneratorDeviceDAL : DtoObjectBaseDAL, IDtoWindGeneratorDeviceDAL
    {
        public DtoWindGeneratorDeviceDAL(IRepositoryDAL inDbService)
        {
            dbService = inDbService;
        }


        #region Create
        public DtoWindGeneratorDeviceResponse Create(DtoWindGeneratorDevice inObject)
        {
            DtoWindGeneratorDeviceResponse toRet = new DtoWindGeneratorDeviceResponse();
            try
            {
                var dbObject = MyMapper.Convert(inObject); //mapper.Map<RepoEventDirectory>(inObject);
                var responseObj = dbService.GetWindGeneratorDeviceDAL().Create(dbObject);
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
                toRet.Message = string.Format("Error create WindGeneratorDevice.");
                toRet.MessageDescription = "Error details: " + Environment.NewLine + ex;
            }
            return toRet;
        }
        #endregion

        #region Delete
        public DtoWindGeneratorDeviceResponse Delete(DtoWindGeneratorDevice inObject)
        {
            return Delete(inObject?.Id ?? 0);
        }
        #endregion

        #region Delete
        public DtoWindGeneratorDeviceResponse Delete(long inObjectId)
        {
            DtoWindGeneratorDeviceResponse toRet = new DtoWindGeneratorDeviceResponse();

            try
            {
                var responseObj = dbService.GetWindGeneratorDeviceDAL().Delete(inObjectId);
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
                toRet.Message = string.Format("Error delete WindGeneratorDevice.");
                toRet.MessageDescription = "Error details: " + Environment.NewLine + ex;
            }
            return toRet;
        }
        #endregion

        #region DeleteWholeTableContent
        public DtoWindGeneratorDeviceResponse DeleteWholeTableContent()
        {
            var responseObj = dbService.GetWindGeneratorDeviceDAL().DeleteWholeTableContent();
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
        public DtoWindGeneratorDeviceResponse Get(long inId)
        {
            DtoWindGeneratorDeviceResponse toRet = new DtoWindGeneratorDeviceResponse();

            try
            {
                var dbObject = dbService.GetWindGeneratorDeviceDAL().Get(inId);
                toRet = MyMapper.Convert(dbObject); //mapper.Map<DtoEventDirectoryResponse>(dbObject);

            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = string.Format("Error get WindGeneratorDevice details.");
                toRet.MessageDescription = "Error details: " + Environment.NewLine + ex;
            }
            return toRet;
        }
        #endregion

        #region GetList
        public DtoWindGeneratorDeviceListResponse GetList(DtoPaging inPaging = null)
        {
            DtoWindGeneratorDeviceListResponse toRet = new DtoWindGeneratorDeviceListResponse();

            try
            {
                RepositoryPaging repopaging = MyMapper.Convert(inPaging); //mapper.Map<Repositorypaging>(inPaging);
                var dbList = dbService.GetWindGeneratorDeviceDAL().GetList(repopaging);//(RepositoryEventDirectoryListResponse)
                toRet = MyMapper.Convert(dbList); //mapper.Map<DtoEventDirectoryListResponse>(dbList);

            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = string.Format("Error query WindGeneratorDevice.");
                toRet.MessageDescription = "Error details: " + Environment.NewLine + ex;
            }
            return toRet;
        }
        #endregion

        #region Update
        public DtoWindGeneratorDeviceResponse Update(DtoWindGeneratorDevice inObject)
        {
            DtoWindGeneratorDeviceResponse toRet = new DtoWindGeneratorDeviceResponse();

            try
            {
                bool objExist = dbService.GetWindGeneratorDeviceDAL().ObjectExist(inObject?.Id ?? 0);
                if (objExist)
                {
                    var dbObject = MyMapper.Convert(inObject); //mapper.Map<RepoEventDirectory>(inObject);
                    var responseObj = dbService.GetWindGeneratorDeviceDAL().Update(dbObject);

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
                    toRet.Message = string.Format("Error update WindGeneratorDevice. WindGeneratorDevice with id: {0}, not found in database.", (inObject?.Id ?? 0));
                }
            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = string.Format("Error update WindGeneratorDevice.");
                toRet.MessageDescription = "Error details: " + Environment.NewLine + ex;
            }

            return toRet;
        }
        #endregion

        #region Soft Delete
        public DtoWindGeneratorDeviceResponse SoftDelete(DtoWindGeneratorDevice inObject)
        {
            return this.SoftDelete(inObject?.Id ?? 0);
        }

        public DtoWindGeneratorDeviceResponse SoftDelete(long inObjectId)
        {
            DtoWindGeneratorDeviceResponse toRet = new DtoWindGeneratorDeviceResponse();

            try
            {
                var responseObj = dbService.GetWindGeneratorDeviceDAL().SoftDelete(inObjectId);
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
                toRet.Message = string.Format("Error soft delete DtoWindGeneratorDevice.");
                toRet.MessageDescription = "Error soft details: " + Environment.NewLine + ex;
            }
            return toRet;
        }
        #endregion
    }
}
