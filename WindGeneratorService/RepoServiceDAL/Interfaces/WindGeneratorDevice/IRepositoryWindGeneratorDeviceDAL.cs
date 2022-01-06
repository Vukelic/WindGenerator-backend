using RepoServiceDAL.Interfaces.Common;
using RepositoryModel.RepoModels.Implementations.WindGeneratorDevice;
using RepositoryModel.RepoResponseObjectModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoServiceDAL.Interfaces.WindGeneratorDevice
{
   public interface IRepositoryWindGeneratorDeviceDAL : IRepositoryObjectBaseDAL<long, RepoWindGeneratorDevice>
    {
        RepositoryResponseBase<RepoWindGeneratorDevice> UpdateBasicInfoGenerator(RepoWindGeneratorDevice generator);
        RepositoryResponseBase<RepoWindGeneratorDevice> UpdatePowerOnGenerator(RepoWindGeneratorDevice generator);
    }
}
