using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace HistoryServiceAPI.Jobs
{
    public class AutomaticHistoryStartStopService : HostedService
    {
        private readonly AutomaticHistoryStartStopProvider _provider;
        private readonly ILog _log;
        public string _gID { get; set; }

        public AutomaticHistoryStartStopService(AutomaticHistoryStartStopProvider provider)
        {
            _provider = provider;
            _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                double timeBeforeAfterMidnight = 60; // 15;
                int delayTime = _provider.DelaySurveysStartStopTimeInterval(timeBeforeAfterMidnight);

                _log.Info(this.GetType().Name + "." + "AutomaticSurveyStartStopService Time delay" + ": Delay time in Automatic Survey Start-Stop Service - in Seconds: " + delayTime);

                await Task.Delay(TimeSpan.FromSeconds(delayTime), cancellationToken);

                await _provider.StoptSurveys(cancellationToken, timeBeforeAfterMidnight);
                await Task.Delay(TimeSpan.FromSeconds(timeBeforeAfterMidnight * 2), cancellationToken);
                await _provider.StartSurveys(cancellationToken, timeBeforeAfterMidnight);
            }
        }
    }
}
