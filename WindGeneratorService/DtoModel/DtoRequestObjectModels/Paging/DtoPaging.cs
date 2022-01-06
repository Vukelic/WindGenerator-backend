using System;
using System.Collections.Generic;
using System.Text;

namespace DtoModel.DtoRequestObjectModels.Paging
{
    public class DtoPaging
    {
        public DtoPaging()
        {
            filters = new Dictionary<string, string>();
            filtersType = new Dictionary<string, string>();
            orders = new Dictionary<string, string>();
        }
        public int page { get; set; }
        public int countPerPage { get; set; }
        public int totalPages { get; set; }
        public int totalCount { get; set; }
        public Dictionary<string, string> filters { get; set; }
        public Dictionary<string, string> filtersType { get; set; }
        public Dictionary<string, string> orders { get; set; }

        public string nextPageToken { get; set; }

        public DtoPaging InsertSoftDeletedFilter(bool inSoftDeleteValue)
        {
            if (filters == null)
            {
                filters = new Dictionary<string, string>();
            }
            if (!filters.ContainsKey("SoftDeleted"))
            {
                filters.Add("SoftDeleted", inSoftDeleteValue.ToString());
            }

            if (filtersType == null)
            {
                filtersType = new Dictionary<string, string>();
            }
            if (!filtersType.ContainsKey("SoftDeleted"))
            {
                filtersType.Add("SoftDeleted", "eq");
            }
            return this;
        }
    }
}
