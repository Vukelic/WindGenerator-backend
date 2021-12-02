using DtoModel.DtoModels.Implementations.WindGeneratorDevice_History;
using DtoModel.DtoResponseObjectModels.WindGeneratorDevice_History;
using RepositoryModel.RepoModels.Implementations.WindGeneratorDevice_History;
using RepositoryModel.RepoResponseObjectModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMapper.CustomMapperFramework
{
    public partial class MyMapper
    {
        #region DtoWindGeneratorDevice_History Convert(RepoWindGeneratorDevice_History inObj)
        public static DtoWindGeneratorDevice_History Convert(RepoWindGeneratorDevice_History inObj)
        {
            DtoWindGeneratorDevice_History toRet = null;
            if (inObj != null)
            {
                toRet = new DtoWindGeneratorDevice_History()
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
                    GeographicalLongitudeStr = inObj.GeographicalLongitudeStr,
                    GeographicalLongitude = inObj.GeographicalLongitude,
                    GeographicalLatitude = inObj.GeographicalLatitude,
                    GeographicalLatitudeStr = inObj.GeographicalLatitudeStr
                    // COMPLEX
                    //COMPLEX LIST


                    //System - specific


                };
            }
            return toRet;
        }

        #endregion

        #region List<DtoWindGeneratorDevice_History> Convert(List<RepoWindGeneratorDevice_History> inObj)
        public static List<DtoWindGeneratorDevice_History> Convert(List<RepoWindGeneratorDevice_History> inObj)
        {
            List<DtoWindGeneratorDevice_History> toRet = null;
            if (inObj != null)
            {
                toRet = new List<DtoWindGeneratorDevice_History>();
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

        #region RepoWindGeneratorDevice_History Convert(DtoWindGeneratorDevice_History inObj)
        public static RepoWindGeneratorDevice_History Convert(DtoWindGeneratorDevice_History inObj)
        {
            RepoWindGeneratorDevice_History toRet = null;
            if (inObj != null)
            {
                toRet = new RepoWindGeneratorDevice_History()
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
                    GeographicalLongitudeStr = inObj.GeographicalLongitudeStr,
                    GeographicalLongitude = inObj.GeographicalLongitude,
                    GeographicalLatitude = inObj.GeographicalLatitude,
                    GeographicalLatitudeStr = inObj.GeographicalLatitudeStr

                    //COMPLEX LIST
                    //TODO
                    //System - specific

                };
            }
            return toRet;
        }
        #endregion

        #region  List<RepoWindGeneratorDevice_History> Convert(List<DtoWindGeneratorDevice_History> inObj)
        public static List<RepoWindGeneratorDevice_History> Convert(List<DtoWindGeneratorDevice_History> inObj)
        {
            List<RepoWindGeneratorDevice_History> toRet = null;
            if (inObj != null)
            {
                toRet = new List<RepoWindGeneratorDevice_History>();
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

        #region  DtoWindGeneratorDevice_HistoryResponse Convert(RepositoryResponseBase<RepoWindGeneratorDevice_History> inObj)
        public static DtoWindGeneratorDevice_HistoryResponse Convert(RepositoryResponseBase<RepoWindGeneratorDevice_History> inObj)
        {
            DtoWindGeneratorDevice_HistoryResponse toRet = null;
            if (inObj != null)
            {
                toRet = new DtoWindGeneratorDevice_HistoryResponse()
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

        #region DtoWindGeneratorDevice_HistoryListResponse Convert(RepositoryResponseBase<IEnumerable<RepoWindGeneratorDevice_History>> inObj)
        public static DtoWindGeneratorDevice_HistoryListResponse Convert(RepositoryResponseBase<IEnumerable<RepoWindGeneratorDevice_History>> inObj)
        {
            DtoWindGeneratorDevice_HistoryListResponse toRet = null;
            if (inObj != null)
            {
                toRet = new DtoWindGeneratorDevice_HistoryListResponse()
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

        #region RepositoryResponseBase<RepoWindGeneratorDevice_History> Convert(DtoWindGeneratorDevice_HistoryResponse inObj)
        public static RepositoryResponseBase<RepoWindGeneratorDevice_History> Convert(DtoWindGeneratorDevice_HistoryResponse inObj)
        {
            RepositoryResponseBase<RepoWindGeneratorDevice_History> toRet = null;
            if (inObj != null)
            {
                toRet = new RepositoryResponseBase<RepoWindGeneratorDevice_History>()
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

        #region  RepositoryResponseBase<IEnumerable<RepoWindGeneratorDevice_History>> Convert(DtoWindGeneratorDevice_HistoryListResponse inObj)
        public static RepositoryResponseBase<IEnumerable<RepoWindGeneratorDevice_History>> Convert(DtoWindGeneratorDevice_HistoryListResponse inObj)
        {
            RepositoryResponseBase<IEnumerable<RepoWindGeneratorDevice_History>> toRet = null;
            if (inObj != null)
            {
                toRet = new RepositoryResponseBase<IEnumerable<RepoWindGeneratorDevice_History>>()
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
