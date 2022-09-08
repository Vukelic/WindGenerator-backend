using HistoryServiceAPI.Models.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HistoryServiceAPI.Interfaces
{
    public interface IHistoryService
    {
        Task<AutoHistoryStartStopProcessResult> AutoStartHistoryAsync(double secondsOnTopOfMidnight = 15);
        Task<AutoHistoryStartStopProcessResult> AutoStopHistoryAsync(double secondsOnTopOfMidnight = 15);
    }
}
