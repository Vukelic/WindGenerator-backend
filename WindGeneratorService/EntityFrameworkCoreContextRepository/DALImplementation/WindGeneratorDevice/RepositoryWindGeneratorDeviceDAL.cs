using EntityFrameworkCoreContextRepository.Context;
using EntityFrameworkCoreContextRepository.DALImplementation.Common;
using RepoServiceDAL.Interfaces.WindGeneratorDevice;
using RepositoryModel.RepoModels.Implementations.WindGeneratorDevice;
using RepositoryModel.RepoResponseObjectModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFrameworkCoreContextRepository.DALImplementation.WindGeneratorDevice
{
    public class RepositoryWindGeneratorDeviceDAL : RepositoryEntityObjectBaseDAL<RepoWindGeneratorDevice>, IRepositoryWindGeneratorDeviceDAL
    {
        public RepositoryWindGeneratorDeviceDAL(WindServiceMainDbContext inDb) : base(inDb)
        {

        }

        #region UpdateBasicInfoGenerator
        public RepositoryResponseBase<RepoWindGeneratorDevice> UpdateBasicInfoGenerator(RepoWindGeneratorDevice generator)
        {
            RepositoryResponseBase<RepoWindGeneratorDevice> toRet = new RepositoryResponseBase<RepoWindGeneratorDevice>();
            try
            {
                var dbObj = db.WindGeneratorDevices.FirstOrDefault(p => p.Id == generator.Id);
                if (dbObj != null)
                {
                    dbObj.Name = generator.Name;
                    dbObj.GeographicalLatitude = generator.GeographicalLatitude;
                    dbObj.GeographicalLongitude = generator.GeographicalLongitude;
                    dbObj.City = generator.City;
                    dbObj.Country = generator.Country;
                    dbObj.Description = generator.Description;

                    toRet.Success = true;
                    toRet.Value = dbObj;
                }
            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = ex.Message;
                toRet.MessageDescription = "Detail error: " + ex;
                toRet.Value = null;
            }

            return toRet;
        }
        #endregion

        #region UpdatePowerOnGenerator
        public RepositoryResponseBase<RepoWindGeneratorDevice> UpdatePowerOnGenerator(RepoWindGeneratorDevice generator)
        {
            RepositoryResponseBase<RepoWindGeneratorDevice> toRet = new RepositoryResponseBase<RepoWindGeneratorDevice>();
            try
            {
                var dbObj = db.WindGeneratorDevices.FirstOrDefault(p => p.Id == generator.Id);
                if (dbObj != null)
                {
                    dbObj.ValueDec = generator.ValueDec;
                    dbObj.ValueStr = generator.ValueStr;
                
                    toRet.Success = true;
                    toRet.Value = dbObj;
                }
            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = ex.Message;
                toRet.MessageDescription = "Detail error: " + ex;
                toRet.Value = null;
            }

            return toRet;
        } 
        #endregion
    }

}
