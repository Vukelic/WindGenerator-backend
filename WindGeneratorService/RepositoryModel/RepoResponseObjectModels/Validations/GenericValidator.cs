using RepositoryModel.RepoModels.Abstractions.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RepositoryModel.RepoResponseObjectModels.Validations
{
    public class GenericValidator
    {
        public static bool Validate(ARepoBaseEntity inObject, List<ValidationResult> inValResult)
        {
            if (inObject != null)
            {
                /*if (typeof(RepoEventHappening) == (inObject.GetType()))
                {
                    return RepoEventHappeningValidator.Validate(inObject as RepoEventHappening, inValResult);
                }*/

                //if (typeof(DbCampaign) == (inObject.GetType()))
                //{
                //    return DbCampaignValidator.Validate(inObject as DbCampaign, inValResult);
                //}
                //else if (typeof(DbDefaultResponse) == (inObject.GetType()))
                //{
                //    return DbDefaultResponseValidator.Validate(inObject as DbDefaultResponse, inValResult);
                //}
                //else if (typeof(DbFacebookMessengerResponse) == (inObject.GetType()))
                //{
                //    return DbFacebookMessengerResponseValidator.Validate(inObject as DbFacebookMessengerResponse, inValResult);
                //}
                //else if (typeof(DbFacebookMessengerResponseAction) == (inObject.GetType()))
                //{
                //    return DbFacebookMessengerResponseActionValidator.Validate(inObject as DbFacebookMessengerResponseAction, inValResult);
                //}
                //else if (typeof(DbMarket) == (inObject.GetType()))
                //{
                //    return DbMarketValidator.Validate(inObject as DbMarket, inValResult);
                //} 
            }

            return true;
        }
    }
}
