using EntityFrameworkCoreContextRepository.Context;
using EntityFrameworkCoreContextRepository.DALImplementation.Common;
using RepoServiceDAL.Interfaces.WindGeneratorDevice;
using RepositoryModel.RepoModels.Implementations.WindGeneratorDevice;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkCoreContextRepository.DALImplementation.WindGeneratorDevice
{
    public class RepositoryWindGeneratorDeviceDAL : RepositoryEntityObjectBaseDAL<RepoWindGeneratorDevice>, IRepositoryWindGeneratorDeviceDAL
    {
        public RepositoryWindGeneratorDeviceDAL(WindServiceMainDbContext inDb) : base(inDb)
        {

        }

    }

}
