using DtoModel.DtoModels.Implementations.WindGeneratorDevice;
using DtoModel.DtoResponseObjectModels.WindGeneratorDevice;
using RepositoryModel.RepoModels.Implementations.WindGeneratorDevice;
using RepositoryModel.RepoResponseObjectModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMapper.CustomMapperFramework
{
    public partial class MyMapper
    {
        #region DtoWindGeneratorDevice Convert(RepoWindGeneratorDevice inObj)
        public static DtoWindGeneratorDevice Convert(RepoWindGeneratorDevice inObj)
        {
            DtoWindGeneratorDevice toRet = null;
            if (inObj != null)
            {
                toRet = new DtoWindGeneratorDevice()
                {

                    Id = inObj.Id,
                    SoftDeleted = inObj.SoftDeleted,
                    TimeCreated = inObj.TimeCreated,
                    TimeModified = inObj.TimeModified,
                    CreatedUserId = inObj.CreatedUserId,
                    CreatedUserName = inObj.CreatedUserName,
                    ModifiedUserId = inObj.ModifiedUserId,
                    ModifiedUserName = inObj.ModifiedUserName,
                    SystemString = inObj.SystemString,
                    AdditionalJsonData = inObj.AdditionalJsonData,
                    IsVirtual = inObj.IsVirtual,
                    SoftDeleteReasonInt = inObj.SoftDeleteReasonInt,
                    SoftDeleteReasonJson = inObj.SoftDeleteReasonJson,

                    Name = inObj.Name,
                    Description = inObj.Description,
                    ValueDec = inObj.ValueDec,
                    ValueStr = inObj.ValueStr,
                    GeographicalLatitude = inObj.GeographicalLatitude,
                    GeographicalLatitudeStr = inObj.GeographicalLatitudeStr,
                    GeographicalLongitude = inObj.GeographicalLongitude,
                    GeographicalLongitudeStr = inObj.GeographicalLongitudeStr,
                    City = inObj.City,
                    Country = inObj.Country
                    // COMPLEX
                    //COMPLEX LIST


                    //System - specific


                };
            }
            return toRet;
        }

        #endregion

        #region List<DtoWindGeneratorDevice> Convert(List<RepoWindGeneratorDevice> inObj)
        public static List<DtoWindGeneratorDevice> Convert(List<RepoWindGeneratorDevice> inObj)
        {
            List<DtoWindGeneratorDevice> toRet = null;
            if (inObj != null)
            {
                toRet = new List<DtoWindGeneratorDevice>();
                if (inObj.Count > 0)
                {
                    foreach (var item in inObj)
                    {
                        if (item != null)
                        {
                            toRet.Add(Convert(item));
                        }
                    }
                }
            }
            return toRet;
        }
        #endregion

        #region RepoWindGeneratorDevice Convert(DtoWindGeneratorDevice inObj)
        public static RepoWindGeneratorDevice Convert(DtoWindGeneratorDevice inObj)
        {
            RepoWindGeneratorDevice toRet = null;
            if (inObj != null)
            {
                toRet = new RepoWindGeneratorDevice()
                {

                    Id = inObj.Id,
                    SoftDeleted = inObj.SoftDeleted,
                    TimeCreated = inObj.TimeCreated,
                    TimeModified = inObj.TimeModified,
                    CreatedUserId = inObj.CreatedUserId,
                    CreatedUserName = inObj.CreatedUserName,
                    ModifiedUserId = inObj.ModifiedUserId,
                    ModifiedUserName = inObj.ModifiedUserName,
                    SystemString = inObj.SystemString,
                    AdditionalJsonData = inObj.AdditionalJsonData,
                    IsVirtual = inObj.IsVirtual,
                    SoftDeleteReasonInt = inObj.SoftDeleteReasonInt,
                    SoftDeleteReasonJson = inObj.SoftDeleteReasonJson,

                    Name = inObj.Name,
                    Description = inObj.Description,
                    ValueDec = inObj.ValueDec,
                    ValueStr = inObj.ValueStr,
                    GeographicalLatitude = inObj.GeographicalLatitude,
                    GeographicalLatitudeStr = inObj.GeographicalLatitudeStr,
                    GeographicalLongitude = inObj.GeographicalLongitude,
                    GeographicalLongitudeStr = inObj.GeographicalLongitudeStr,
                    City = inObj.City,
                    Country = inObj.Country


                    //COMPLEX LIST
                    //TODO
                    //System - specific

                };
            }
            return toRet;
        }
        #endregion

        #region  List<RepoWindGeneratorDevice> Convert(List<DtoWindGeneratorDevice> inObj)
        public static List<RepoWindGeneratorDevice> Convert(List<DtoWindGeneratorDevice> inObj)
        {
            List<RepoWindGeneratorDevice> toRet = null;
            if (inObj != null)
            {
                toRet = new List<RepoWindGeneratorDevice>();
                if (inObj.Count > 0)
                {
                    foreach (var item in inObj)
                    {
                        if (item != null)
                        {
                            toRet.Add(Convert(item));
                        }
                    }
                }
            }
            return toRet;
        }
        #endregion

        #region  DtoWindGeneratorDeviceResponse Convert(RepositoryResponseBase<RepoWindGeneratorDevice> inObj)
        public static DtoWindGeneratorDeviceResponse Convert(RepositoryResponseBase<RepoWindGeneratorDevice> inObj)
        {
            DtoWindGeneratorDeviceResponse toRet = null;
            if (inObj != null)
            {
                toRet = new DtoWindGeneratorDeviceResponse()
                {
                    Message = inObj.Message,
                    MessageDescription = inObj.MessageDescription,
                    Success = inObj.Success,
                    Value = Convert(inObj.Value),
                    PagingObject = Convert(inObj.PaggingObject),
                };
            }
            return toRet;
        }
        #endregion

        #region DtoWindGeneratorDeviceListResponse Convert(RepositoryResponseBase<IEnumerable<RepoWindGeneratorDevice>> inObj)
        public static DtoWindGeneratorDeviceListResponse Convert(RepositoryResponseBase<IEnumerable<RepoWindGeneratorDevice>> inObj)
        {
            DtoWindGeneratorDeviceListResponse toRet = null;
            if (inObj != null)
            {
                toRet = new DtoWindGeneratorDeviceListResponse()
                {
                    Message = inObj.Message,
                    MessageDescription = inObj.MessageDescription,
                    Success = inObj.Success,
                    Value = Convert(inObj.Value?.ToList()),
                    PagingObject = Convert(inObj.PaggingObject),
                };
            }
            return toRet;
        }
        #endregion

        #region RepositoryResponseBase<RepoWindGeneratorDevice> Convert(DtoWindGeneratorDeviceResponse inObj)
        public static RepositoryResponseBase<RepoWindGeneratorDevice> Convert(DtoWindGeneratorDeviceResponse inObj)
        {
            RepositoryResponseBase<RepoWindGeneratorDevice> toRet = null;
            if (inObj != null)
            {
                toRet = new RepositoryResponseBase<RepoWindGeneratorDevice>()
                {
                    Message = inObj.Message,
                    MessageDescription = inObj.MessageDescription,
                    Success = inObj.Success,
                    Value = Convert(inObj.Value),
                    PaggingObject = Convert(inObj.PagingObject),
                };
            }
            return toRet;
        }
        #endregion

        #region  RepositoryResponseBase<IEnumerable<RepoWindGeneratorDevice>> Convert(DtoWindGeneratorDeviceListResponse inObj)
        public static RepositoryResponseBase<IEnumerable<RepoWindGeneratorDevice>> Convert(DtoWindGeneratorDeviceListResponse inObj)
        {
            RepositoryResponseBase<IEnumerable<RepoWindGeneratorDevice>> toRet = null;
            if (inObj != null)
            {
                toRet = new RepositoryResponseBase<IEnumerable<RepoWindGeneratorDevice>>()
                {
                    Message = inObj.Message,
                    MessageDescription = inObj.MessageDescription,
                    Success = inObj.Success,
                    Value = Convert(inObj.Value?.ToList()),
                    PaggingObject = Convert(inObj.PagingObject),
                };
            }
            return toRet;
        }
        #endregion

    }
}
