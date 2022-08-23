using EntityFrameworkCoreContextRepository.Context;
using EntityFrameworkCoreContextRepository.DALImplementation.Common;
using RepoServiceDAL.Interfaces.WindGeneratorType;
using RepositoryModel.RepoModels.Implementations.WindGeneratorType;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkCoreContextRepository.DALImplementation.WindGeneratorType
{
    public class RepositoryWindGeneratorTypeDAL : RepositoryEntityObjectBaseDAL<RepoWindGeneratorType>, IRepositoryWindGeneratorTypeDAL
    {
        public RepositoryWindGeneratorTypeDAL(WindServiceMainDbContext inDb) : base(inDb)
        {

        }

    }
}
