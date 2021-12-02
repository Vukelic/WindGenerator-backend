using EntityFrameworkCoreContextRepository.Context;
using EntityFrameworkCoreContextRepository.DALImplementation.Common;
using RepoServiceDAL.Interfaces.Role;
using RepositoryModel.RepoModels.Implementations.Role;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkCoreContextRepository.DALImplementation.Role
{
    public class RepositoryRoleDAL : RepositoryEntityObjectBaseDAL<RepoRole>, IRepositoryRoleDAL
    {

        public RepositoryRoleDAL(WindServiceMainDbContext inDb) : base(inDb)
        {

        }
    }
}
