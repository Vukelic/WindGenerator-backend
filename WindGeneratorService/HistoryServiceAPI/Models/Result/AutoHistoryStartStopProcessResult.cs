using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HistoryServiceAPI.Models.Result
{
    public class AutoHistoryStartStopProcessResult
    {
        public string ProcessType { get; set; } // Start ili Stop
        public int StartedStoppedSurveyNumber { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
