
using EntityFrameworkCoreContextRepository.Context;
using EntityFrameworkCoreContextRepository.DALImplementation.Common;
using RepoServiceDAL.Interfaces.WindGeneratorDevice_History;
using RepositoryModel.RepoModels.Implementations.WindGeneratorDevice_History;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkCoreContextRepository.DALImplementation.WindGeneratorDevice_History
{
   public class RepositoryWindGeneratorDevice_HistoryDAL : RepositoryEntityObjectBaseDAL<RepoWindGeneratorDevice_History>, IRepositoryWindGeneratorDevice_HistoryDAL
    {
        public RepositoryWindGeneratorDevice_HistoryDAL(WindServiceMainDbContext inDb) : base(inDb)
        {

        }

    }
}
