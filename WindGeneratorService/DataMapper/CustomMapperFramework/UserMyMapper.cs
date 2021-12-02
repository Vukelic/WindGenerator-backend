using DtoModel.DtoModels.Implementations.User;
using DtoModel.DtoResponseObjectModels.User;
using RepositoryModel.RepoModels.Implementations.User;
using RepositoryModel.RepoResponseObjectModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMapper.CustomMapperFramework
{
    public partial class MyMapper
    {
        #region  DtoUser Convert(RepoUser inObj)
        public static DtoUser Convert(RepoUser inObj)
        {
            DtoUser toRet = null;
            if (inObj != null)
            {
                toRet = new DtoUser()
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
                    UserName = inObj.UserName,
                    Susspend = inObj.Susspend,
                    Surname = inObj.Surname,
                    Phone = inObj.Phone,

                    UserToken = inObj.UserToken,
                    ExpireTokenDateTime = inObj.ExpireTokenDateTime,
                    Workplace = inObj.Workplace,
                    EmailConfirmed = inObj.EmailConfirmed,
                    FailedAttempt = inObj.FailedAttempt,
                    StartTrackingInterval = inObj.StartTrackingInterval,
                    LastLoginTime = inObj.LastLoginTime,
                    AppFlag = inObj.AppFlag,
                    RssId = inObj.RssId,
                    AssignRoleId = inObj.AssignRoleId,

                    // COMPLEX
                    //COMPLEX LIST


                    //System - specific


                };
            }
            return toRet;
        }
        #endregion

        #region List<DtoUser> Convert(List<RepoUser> inObj)
        public static List<DtoUser> Convert(List<RepoUser> inObj)
        {
            List<DtoUser> toRet = null;
            if (inObj != null)
            {
                toRet = new List<DtoUser>();
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

        #region  RepoUser Convert(DtoUser inObj)
        public static RepoUser Convert(DtoUser inObj)
        {
            RepoUser toRet = null;
            if (inObj != null)
            {
                toRet = new RepoUser()
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
                    UserName = inObj.UserName,
                    Susspend = inObj.Susspend,
                    Surname = inObj.Surname,
                    Phone = inObj.Phone,

                    UserToken = inObj.UserToken,
                    ExpireTokenDateTime = inObj.ExpireTokenDateTime,
                    Workplace = inObj.Workplace,
                    EmailConfirmed = inObj.EmailConfirmed,
                    FailedAttempt = inObj.FailedAttempt,
                    StartTrackingInterval = inObj.StartTrackingInterval,
                    LastLoginTime = inObj.LastLoginTime,
                    AppFlag = inObj.AppFlag,
                    RssId = inObj.RssId,
                    AssignRoleId = inObj.AssignRoleId,




                    //COMPLEX LIST
                    //TODO
                    //System - specific

                };
            }
            return toRet;
        }
        #endregion

        #region List<RepoUser> Convert(List<DtoUser> inObj)
        public static List<RepoUser> Convert(List<DtoUser> inObj)
        {
            List<RepoUser> toRet = null;
            if (inObj != null)
            {
                toRet = new List<RepoUser>();
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

        #region DtoUserResponse Convert(RepositoryResponseBase<RepoUser> inObj)
        public static DtoUserResponse Convert(RepositoryResponseBase<RepoUser> inObj)
        {
            DtoUserResponse toRet = null;
            if (inObj != null)
            {
                toRet = new DtoUserResponse()
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

        #region DtoUserListResponse Convert(RepositoryResponseBase<IEnumerable<RepoUser>> inObj)
        public static DtoUserListResponse Convert(RepositoryResponseBase<IEnumerable<RepoUser>> inObj)
        {
            DtoUserListResponse toRet = null;
            if (inObj != null)
            {
                toRet = new DtoUserListResponse()
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

        #region  RepositoryResponseBase<RepoUser> Convert(DtoUserResponse inObj)
        public static RepositoryResponseBase<RepoUser> Convert(DtoUserResponse inObj)
        {
            RepositoryResponseBase<RepoUser> toRet = null;
            if (inObj != null)
            {
                toRet = new RepositoryResponseBase<RepoUser>()
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

        #region  RepositoryResponseBase<IEnumerable<RepoUser>> Convert(DtoUserListResponse inObj)
        public static RepositoryResponseBase<IEnumerable<RepoUser>> Convert(DtoUserListResponse inObj)
        {
            RepositoryResponseBase<IEnumerable<RepoUser>> toRet = null;
            if (inObj != null)
            {
                toRet = new RepositoryResponseBase<IEnumerable<RepoUser>>()
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

        #region  DtoUser ConvertUserWithRole(RepoUser inObj)
        public static List<DtoUser> ConvertUserWithRole(List<RepoUser> inObj)
        {
            List<DtoUser> toRet = null;
            if (inObj != null)
            {
                toRet = new List<DtoUser>();
                if (inObj.Count > 0)
                {
                    foreach (var item in inObj)
                    {
                        if (item != null)
                        {
                            var dtoUser = Convert(item);
                            toRet.Add(dtoUser);
                            if (item.AssignRole != null)
                            {

                                dtoUser.AssignRole = MyMapper.Convert(item.AssignRole);
                            }

                        }
                    }
                }
            }
            return toRet;
        }
        #endregion

        #region DtoUserListResponse Convert(RepositoryResponseBase<IEnumerable<RepoUser>> inObj)
        public static DtoUserListResponse ConvertListUsersWithRole(RepositoryResponseBase<IEnumerable<RepoUser>> inObj)
        {
            DtoUserListResponse toRet = null;
            if (inObj != null)
            {
                toRet = new DtoUserListResponse()
                {
                    Message = inObj.Message,
                    MessageDescription = inObj.MessageDescription,
                    Success = inObj.Success,
                    Value = ConvertUserWithRole(inObj.Value?.ToList()),
                    PagingObject = Convert(inObj.PaggingObject),
                };
            }
            return toRet;
        }
        #endregion

        #region DtoUserResponse Convert(RepositoryResponseBase<RepoUser> inObj)
        public static DtoUserResponse ConvertUserWithRole(RepositoryResponseBase<RepoUser> inObj)
        {
            DtoUserResponse toRet = null;
            if (inObj != null)
            {
                toRet = MyMapper.Convert(inObj);

            }
            if (inObj.Value.AssignRole != null)
            {
                var dtoRole = MyMapper.Convert(inObj.Value.AssignRole);
                toRet.Value.AssignRole = dtoRole;
            }


            return toRet;
        }
        #endregion
    }
}
