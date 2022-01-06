using RepoServiceDAL.Interfaces;
using RepoServiceDAL.Interfaces.Role;
using RepoServiceDAL.Interfaces.User;
using RepoServiceDAL.Interfaces.WindGeneratorDevice;
using RepoServiceDAL.Interfaces.WindGeneratorDevice_History;
using RepositoryModel.RepoModels.Abstractions.Common;
using RepositoryModel.RepoResponseObjectModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoServiceDAL.Abstractions
{
    public abstract class ARepositoryDAL : IRepositoryDAL
    {
        protected ARepositoryDAL()
        {

        }
        public abstract RepositoryResponseBase<long> SaveChanges();
        public abstract object GetRepositoryContext();
        public abstract void UpdateObjectPropertiesFromDb(ARepoBaseEntity inObject, string inPropertyName = null);

        public IRepositoryUserDAL UserDALService { get; set; }
        public virtual IRepositoryUserDAL GetUserDAL()
        {
            return UserDALService;
        }

        public IRepositoryRoleDAL RoleDALService { get; set; }
        public virtual IRepositoryRoleDAL GetRoleDAL()
        {
            return RoleDALService;
        }

        public IRepositoryWindGeneratorDeviceDAL WindGeneratorDeviceDALService { get; set; }
        public virtual IRepositoryWindGeneratorDeviceDAL GetWindGeneratorDeviceDAL()
        {
            return WindGeneratorDeviceDALService;
        }

        public IRepositoryWindGeneratorDevice_HistoryDAL WindGeneratorDevice_HistoryDALService { get; set; }
        public virtual IRepositoryWindGeneratorDevice_HistoryDAL GetWindGeneratorDevice_HistoryDAL()
        {
            return WindGeneratorDevice_HistoryDALService;
        }
    }
}
