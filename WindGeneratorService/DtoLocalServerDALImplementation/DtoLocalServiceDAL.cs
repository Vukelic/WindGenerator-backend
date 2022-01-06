using DtoLocalServerDALImplementation.DALImplementation.Role;
using DtoLocalServerDALImplementation.DALImplementation.User;
using DtoLocalServerDALImplementation.DALImplementation.WindGeneratorDevice;
using DtoLocalServerDALImplementation.DALImplementation.WindGeneratorDevice_History_History;
using DtoServiceDAL.Abstractions;
using EntityFrameworkCoreContextRepository.DALImplementation;
using RepoServiceDAL.Interfaces;
using System;

namespace DtoLocalServerDALImplementation
{
    public class DtoLocalServiceDAL : ADtoDAL
    {
        protected IRepositoryDAL dbService;
        public string ActiveConnectionString { get; set; }
        public DtoLocalServiceDAL(string inConnectionStringForUnitTest = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(inConnectionStringForUnitTest))
                {
                    //ActiveConnectionString = @ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    throw new Exception("no connection string!");
                }
                else
                {
                    ActiveConnectionString = inConnectionStringForUnitTest;
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"DtoLocalServiceDAL failed to initialize. Connection string could not be loaded. Additional info: {ex.Message}", ex);
            }

            dbService = new EntityFrameworkCoreDAL(ActiveConnectionString);
            RoleDALService = new DtoRoleDAL(dbService);
            UserDALService = new DtoUserDAL(dbService);
            WindGeneratorDeviceDALService = new DtoWindGeneratorDeviceDAL(dbService);
            WindGeneratorDevice_HistoryDALService = new DtoWindGeneratorDevice_HistoryDAL(dbService);
         
        }
    }
}
