using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace RepositoryModel.RepoResponseObjectModels.Validations.Helpers
{
    public class ValidarionResultConverter
    {
        public static string GetMessageDescription(List<ValidationResult> inValResult)
        {
            string toRet = "Details: ";
            if (inValResult != null && inValResult.Count > 0)
            {
                foreach (var item in inValResult)
                {
                    if (item != null)
                    {
                        toRet += item.ErrorMessage;
                    }
                }
            }
            return toRet;
        }
    }
}
