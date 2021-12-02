using RepositoryModel.RepoRequestObjectModels.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryModel.RepoRequestObjectModels.Paging
{
    public class RepositoryPaging
    {
        public RepositoryPaging()
        {
            filters = new Dictionary<string, string>();
            filtersType = new Dictionary<string, string>();
            orders = new Dictionary<string, string>();
            countPerPage = 100;
            page = 1;
        }
        public int page { get; set; }
        public int countPerPage { get; set; }
        public int totalPages { get; set; }
        public int totalCount { get; set; }
        public Dictionary<string, string> filters { get; set; }
        public Dictionary<string, string> filtersType { get; set; }
        public Dictionary<string, string> orders { get; set; }


        private List<RepositoryFilter> repositoryFilters = null;

        public List<RepositoryFilter> RepositoryFilters
        {
            get
            {
                if (repositoryFilters == null)
                {
                    repositoryFilters = RepositoryFilterConverter.ConvertToRepositoryFilters(filters, filtersType, orders);
                }
                return repositoryFilters;
            }
            set
            {
                repositoryFilters = value;
            }
        }


    }
}
