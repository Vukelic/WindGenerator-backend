using DtoModel.DtoModels.Implementations.Role;
using DtoModel.DtoResponseObjectModels.Role;
using RepositoryModel.RepoModels.Implementations.Role;
using RepositoryModel.RepoResponseObjectModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMapper.CustomMapperFramework
{
    public partial class MyMapper
    {
        #region DtoRole Convert(RepoRole inObj)
        public static DtoRole Convert(RepoRole inObj)
        {
            DtoRole toRet = null;
            if (inObj != null)
            {
                toRet = new DtoRole()
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
                    Active = inObj.Active,
                
                    // COMPLEX
                    //COMPLEX LIST


                    //System - specific


                };
            }
            return toRet;
        }

        #endregion

        #region List<DtoRole> Convert(List<RepoRole> inObj)
        public static List<DtoRole> Convert(List<RepoRole> inObj)
        {
            List<DtoRole> toRet = null;
            if (inObj != null)
            {
                toRet = new List<DtoRole>();
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

        #region RepoRole Convert(DtoRole inObj)
        public static RepoRole Convert(DtoRole inObj)
        {
            RepoRole toRet = null;
            if (inObj != null)
            {
                toRet = new RepoRole()
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
                    Active = inObj.Active,
                 

                    //COMPLEX LIST
                    //TODO
                    //System - specific

                };
            }
            return toRet;
        }
        #endregion

        #region  List<RepoRole> Convert(List<DtoRole> inObj)
        public static List<RepoRole> Convert(List<DtoRole> inObj)
        {
            List<RepoRole> toRet = null;
            if (inObj != null)
            {
                toRet = new List<RepoRole>();
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

        #region  DtoRoleResponse Convert(RepositoryResponseBase<RepoRole> inObj)
        public static DtoRoleResponse Convert(RepositoryResponseBase<RepoRole> inObj)
        {
            DtoRoleResponse toRet = null;
            if (inObj != null)
            {
                toRet = new DtoRoleResponse()
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

        #region DtoRoleListResponse Convert(RepositoryResponseBase<IEnumerable<RepoRole>> inObj)
        public static DtoRoleListResponse Convert(RepositoryResponseBase<IEnumerable<RepoRole>> inObj)
        {
            DtoRoleListResponse toRet = null;
            if (inObj != null)
            {
                toRet = new DtoRoleListResponse()
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

        #region RepositoryResponseBase<RepoRole> Convert(DtoRoleResponse inObj)
        public static RepositoryResponseBase<RepoRole> Convert(DtoRoleResponse inObj)
        {
            RepositoryResponseBase<RepoRole> toRet = null;
            if (inObj != null)
            {
                toRet = new RepositoryResponseBase<RepoRole>()
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

        #region  RepositoryResponseBase<IEnumerable<RepoRole>> Convert(DtoRoleListResponse inObj)
        public static RepositoryResponseBase<IEnumerable<RepoRole>> Convert(DtoRoleListResponse inObj)
        {
            RepositoryResponseBase<IEnumerable<RepoRole>> toRet = null;
            if (inObj != null)
            {
                toRet = new RepositoryResponseBase<IEnumerable<RepoRole>>()
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
