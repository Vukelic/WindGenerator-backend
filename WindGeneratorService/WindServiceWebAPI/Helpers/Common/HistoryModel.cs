using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WindServiceWebAPI.Helpers.Common.Models;

namespace WindServiceWebAPI.Helpers.Common
{
    public class HistoryModel
    {
        public int cod { get; set; }
        public long city_id { get; set; }
        public double calctime { get; set; }
        public List<HistoryItem> result { get; set; }
    }
}
