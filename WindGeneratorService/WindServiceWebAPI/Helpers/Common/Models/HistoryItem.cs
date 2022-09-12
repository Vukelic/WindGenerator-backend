using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WindServiceWebAPI.Helpers.Common.Models
{
    public class HistoryItem
    {
        public int month { get; set; }
        public int day { get; set; }
        public TempItem temp { get; set; }
        public CustomItem pressure { get; set; }
        public CustomItem humidity { get; set; }
        public CustomItem wind { get; set; }
        public CustomItem precipitation { get; set; }
        public CustomItem clouds { get; set; }
    }
}
