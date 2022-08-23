using RepoServiceDAL.Interfaces.Role;
using RepoServiceDAL.Interfaces.User;
using RepoServiceDAL.Interfaces.WindGeneratorDevice;
using RepoServiceDAL.Interfaces.WindGeneratorDevice_History;
using RepoServiceDAL.Interfaces.WindGeneratorType;
using RepositoryModel.RepoModels.Abstractions.Common;
using RepositoryModel.RepoResponseObjectModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoServiceDAL.Interfaces
{
    public interface IRepositoryDAL
    {
        IRepositoryRoleDAL GetRoleDAL();
        IRepositoryUserDAL GetUserDAL();
        IRepositoryWindGeneratorDeviceDAL GetWindGeneratorDeviceDAL();
        IRepositoryWindGeneratorDevice_HistoryDAL GetWindGeneratorDevice_HistoryDAL();
        IRepositoryWindGeneratorTypeDAL GetWindGeneratorTypeDAL();
        RepositoryResponseBase<long> SaveChanges();
        void UpdateObjectPropertiesFromDb(ARepoBaseEntity inObject, string inPropertyName = null);
        object GetRepositoryContext();
    }
}
