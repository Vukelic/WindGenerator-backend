using DtoModel.DtoRequestObjectModels.Paging;
using RepositoryModel.RepoRequestObjectModels.Paging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataMapper.CustomMapperFramework
{
    public partial class MyMapper
    {
        #region  DtoPaging Convert(RepositoryPaging inObj)
        public static DtoPaging Convert(RepositoryPaging inObj)
        {
            DtoPaging toRet = null;
            if (inObj != null)
            {
                toRet = new DtoPaging()
                {
                    countPerPage = inObj.countPerPage,
                    page = inObj.page,
                    totalCount = inObj.totalCount,
                    totalPages = inObj.totalPages,
                    filters = inObj.filters,
                    filtersType = inObj.filtersType,
                    orders = inObj.orders,

                };
            }
            return toRet;
        }
        #endregion

        #region RepositoryPaging Convert(DtoPaging inObj)
        public static RepositoryPaging Convert(DtoPaging inObj)
        {
            RepositoryPaging toRet = null;
            if (inObj != null)
            {
                toRet = new RepositoryPaging()
                {
                    countPerPage = inObj.countPerPage,
                    page = inObj.page,
                    totalCount = inObj.totalCount,
                    totalPages = inObj.totalPages,
                    filters = inObj.filters,
                    filtersType = inObj.filtersType,
                    orders = inObj.orders,
                };
            }
            return toRet;
        }
        #endregion
    }
}
