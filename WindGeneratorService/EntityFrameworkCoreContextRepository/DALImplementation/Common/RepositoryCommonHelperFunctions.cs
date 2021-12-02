using EntityFrameworkCoreContextRepository.Context;
using RepositoryModel.RepoModels.Abstractions.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkCoreContextRepository.DALImplementation.Common
{
    public class RepositoryCommonHelperFunctions
    {
        public static void CustomGetInclude(ARepoBaseEntity inThis, WindServiceMainDbContext dbContext)
        {
            if (inThis != null)
            {
                //if (typeof(DbCampaign) == (inThis.GetType()))
                //{
                //    (inThis as DbCampaign).CustomGetInclude(dbContext);
                //}
                //else if (typeof(DbDefaultResponse) == (inThis.GetType()))
                //{
                //    (inThis as DbDefaultResponse).CustomGetInclude(dbContext);
                //}
                //else if (typeof(DbFacebookMessengerResponse) == (inThis.GetType()))
                //{
                //    (inThis as DbFacebookMessengerResponse).CustomGetInclude(dbContext);
                //}
                //else if (typeof(DbFacebookMessengerResponseAction) == (inThis.GetType()))
                //{
                //    (inThis as DbFacebookMessengerResponseAction).CustomGetInclude(dbContext);
                //}
                //else if (typeof(DbMarket) == (inThis.GetType()))
                //{
                //    (inThis as DbMarket).CustomGetInclude(dbContext);
                //}

            }
        }

        public static void CustomGetInclude(List<ARepoBaseEntity> inThis, WindServiceMainDbContext dbContext)
        {
            if (inThis != null && inThis.Count > 0)
            {
                foreach (var item in inThis)
                {
                    RepositoryCommonHelperFunctions.CustomGetInclude(item, dbContext);
                }
            }
        }
    }
}
