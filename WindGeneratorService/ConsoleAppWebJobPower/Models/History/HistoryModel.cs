using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppWebJobPower.Models.History
{
    public class HistoryModel
    {
        public int cod { get; set; }
        public long city_id { get; set; }
        public double calctime { get; set; }
        public List<HistoryItem> result { get; set; }
    }
}
