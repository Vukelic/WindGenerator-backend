using RepositoryModel.RepoRequestObjectModels.Sort;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryModel.RepoRequestObjectModels.Filters
{
    public class RepositoryFilter
    {
        public string PropName { get; set; }
        public string FilterValue { get; set; }
        public EFilterTypes FilterType { get; set; }
        public bool ApplyFilter { get; set; }
        public ESortTypes SortType { get; set; }
        public bool ApplySort { get; set; }
    }
}
