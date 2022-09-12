using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppWebJobPower.Models.History
{
    public class HistoryItem
    {
        public int month { get; set; }
        public int day { get; set; }
        public TempItem temp { get; set; }
        public PresureItem pressure { get; set; }
        public HumidityItem humidity { get; set; }
        public WindItem wind { get; set; }
        public PrecipitationItem precipitation { get; set; }
        public CloudItem clouds { get; set; }
    }
}
