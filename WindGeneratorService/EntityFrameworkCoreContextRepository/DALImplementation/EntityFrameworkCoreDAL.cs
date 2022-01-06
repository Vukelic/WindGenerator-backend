using EntityFrameworkCoreContextRepository.Context;
using EntityFrameworkCoreContextRepository.DALImplementation.Role;
using EntityFrameworkCoreContextRepository.DALImplementation.User;
using EntityFrameworkCoreContextRepository.DALImplementation.WindGeneratorDevice;
using EntityFrameworkCoreContextRepository.DALImplementation.WindGeneratorDevice_History;
using Microsoft.EntityFrameworkCore;
using RepoServiceDAL.Abstractions;
using RepositoryModel.RepoModels.Abstractions.Common;
using RepositoryModel.RepoResponseObjectModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkCoreContextRepository.DALImplementation
{
    public class EntityFrameworkCoreDAL : ARepositoryDAL
    {
        public WindServiceMainDbContext db { get; set; }
        static DbContextOptions<WindServiceMainDbContext> options;

        public EntityFrameworkCoreDAL(DbContextOptions<WindServiceMainDbContext> options) : base()
        {
            db = new WindServiceMainDbContext(options);

            //IMPLEMENT BELOW FOR ALL MODELS
            UserDALService = new RepositoryUserDAL(db);
            WindGeneratorDeviceDALService = new RepositoryWindGeneratorDeviceDAL(db);
            RoleDALService = new RepositoryRoleDAL(db);
            WindGeneratorDevice_HistoryDALService = new RepositoryWindGeneratorDevice_HistoryDAL(db);          
        }

        public EntityFrameworkCoreDAL(string inConnectionString) : base()
        {
            var myConnectionString = "";

            if (string.IsNullOrWhiteSpace(inConnectionString))
            {
                throw new Exception("no connection string!");
            }
            else
            {
                myConnectionString = inConnectionString;
            }

            DbContextOptions<WindServiceMainDbContext> options = new DbContextOptions<WindServiceMainDbContext>();
            var builder = new DbContextOptionsBuilder<WindServiceMainDbContext>(options);
            builder.UseSqlServer(inConnectionString);
            db = new WindServiceMainDbContext(builder.Options);



            //IMPLEMENT BELOW FOR ALL MODELS
            UserDALService = new RepositoryUserDAL(db);
            WindGeneratorDeviceDALService = new RepositoryWindGeneratorDeviceDAL(db);
            RoleDALService = new RepositoryRoleDAL(db);
            WindGeneratorDevice_HistoryDALService = new RepositoryWindGeneratorDevice_HistoryDAL(db);
        }
        public override RepositoryResponseBase<long> SaveChanges()
        {
            RepositoryResponseBase<long> toRet = new RepositoryResponseBase<long>();
            try
            {
                int updateCount = db.SaveChanges();
                toRet.Success = true;
                toRet.Value = updateCount;
            }
            catch (Exception ex)
            {
                toRet.Success = false;
                toRet.Message = ex.Message;
                toRet.MessageDescription = "Detail error: " + ex;
                toRet.Value = -1;
            }

            return toRet;
        }

        public override void UpdateObjectPropertiesFromDb(ARepoBaseEntity inObject, string inPropertyName = null)
        {
            try
            {
                db.Entry(inObject).Reload();
            }
            catch (Exception ex)
            {

            }

        }

        public override object GetRepositoryContext()
        {
            return db;
        }
    }
}
