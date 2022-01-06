using System;
using DtoServiceDAL.Abstractions;
using RepoServiceDAL.Interfaces;
namespace RepoServiceDAL
{
   public class GlobalWebApiAppDALInstanceSelector
    {
        public static Func<ADtoDAL> GetDtoDALImplementation;
        //public static Func<IRepositoryDAL> GetRepoDALImplementation;
    }
}
