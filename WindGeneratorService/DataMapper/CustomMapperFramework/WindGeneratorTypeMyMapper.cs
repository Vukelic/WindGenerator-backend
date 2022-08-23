using DtoModel.DtoModels.Implementations.WindGeneratorType;
using DtoModel.DtoResponseObjectModels.WindGeneratorType;
using RepositoryModel.RepoModels.Implementations.WindGeneratorType;
using RepositoryModel.RepoResponseObjectModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMapper.CustomMapperFramework
{
    public partial class MyMapper
    {
        #region DtoWindGeneratorType Convert(RepoWindGeneratorType inObj)
        public static DtoWindGeneratorType Convert(RepoWindGeneratorType inObj)
        {
            DtoWindGeneratorType toRet = null;
            if (inObj != null)
            {
                toRet = new DtoWindGeneratorType()
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
                    GeneratorPower = inObj.GeneratorPower,
                    Guarantee = inObj.Guarantee,
                    HeightOfWing = inObj.HeightOfWing,
                    ImageUrl = inObj.ImageUrl,
                    MaxPowerTurbine = inObj.MaxPowerTurbine,
                    MaxSpeedTurbine = inObj.MaxSpeedTurbine,
                    PowerOfTurbines = inObj.PowerOfTurbines,
                    Turbines = inObj.Turbines,
                    Weight = inObj.Weight,
                    WidthOfWing = inObj.WidthOfWing
                    // COMPLEX
                    //COMPLEX LIST


                    //System - specific


                };
            }
            return toRet;
        }

        #endregion

        #region List<DtoWindGeneratorType> Convert(List<RepoWindGeneratorType> inObj)
        public static List<DtoWindGeneratorType> Convert(List<RepoWindGeneratorType> inObj)
        {
            List<DtoWindGeneratorType> toRet = null;
            if (inObj != null)
            {
                toRet = new List<DtoWindGeneratorType>();
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

        #region RepoWindGeneratorType Convert(DtoWindGeneratorType inObj)
        public static RepoWindGeneratorType Convert(DtoWindGeneratorType inObj)
        {
            RepoWindGeneratorType toRet = null;
            if (inObj != null)
            {
                toRet = new RepoWindGeneratorType()
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
                    GeneratorPower = inObj.GeneratorPower,
                    Guarantee = inObj.Guarantee,
                    HeightOfWing = inObj.HeightOfWing,
                    ImageUrl = inObj.ImageUrl,
                    MaxPowerTurbine = inObj.MaxPowerTurbine,
                    MaxSpeedTurbine = inObj.MaxSpeedTurbine,
                    PowerOfTurbines = inObj.PowerOfTurbines,
                    Turbines = inObj.Turbines,
                    Weight = inObj.Weight,
                    WidthOfWing = inObj.WidthOfWing
                    //COMPLEX LIST
                    //TODO
                    //System - specific

                };
            }
            return toRet;
        }
        #endregion

        #region  List<RepoWindGeneratorType> Convert(List<DtoWindGeneratorType> inObj)
        public static List<RepoWindGeneratorType> Convert(List<DtoWindGeneratorType> inObj)
        {
            List<RepoWindGeneratorType> toRet = null;
            if (inObj != null)
            {
                toRet = new List<RepoWindGeneratorType>();
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

        #region  DtoWindGeneratorTypeResponse Convert(RepositoryResponseBase<RepoWindGeneratorType> inObj)
        public static DtoWindGeneratorTypeResponse Convert(RepositoryResponseBase<RepoWindGeneratorType> inObj)
        {
            DtoWindGeneratorTypeResponse toRet = null;
            if (inObj != null)
            {
                toRet = new DtoWindGeneratorTypeResponse()
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

        #region DtoWindGeneratorTypeListResponse Convert(RepositoryResponseBase<IEnumerable<RepoWindGeneratorType>> inObj)
        public static DtoWindGeneratorTypeListResponse Convert(RepositoryResponseBase<IEnumerable<RepoWindGeneratorType>> inObj)
        {
            DtoWindGeneratorTypeListResponse toRet = null;
            if (inObj != null)
            {
                toRet = new DtoWindGeneratorTypeListResponse()
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

        #region RepositoryResponseBase<RepoWindGeneratorType> Convert(DtoWindGeneratorTypeResponse inObj)
        public static RepositoryResponseBase<RepoWindGeneratorType> Convert(DtoWindGeneratorTypeResponse inObj)
        {
            RepositoryResponseBase<RepoWindGeneratorType> toRet = null;
            if (inObj != null)
            {
                toRet = new RepositoryResponseBase<RepoWindGeneratorType>()
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

        #region  RepositoryResponseBase<IEnumerable<RepoWindGeneratorType>> Convert(DtoWindGeneratorTypeListResponse inObj)
        public static RepositoryResponseBase<IEnumerable<RepoWindGeneratorType>> Convert(DtoWindGeneratorTypeListResponse inObj)
        {
            RepositoryResponseBase<IEnumerable<RepoWindGeneratorType>> toRet = null;
            if (inObj != null)
            {
                toRet = new RepositoryResponseBase<IEnumerable<RepoWindGeneratorType>>()
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
