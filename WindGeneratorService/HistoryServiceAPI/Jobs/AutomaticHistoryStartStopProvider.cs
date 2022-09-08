using HistoryServiceAPI.Interfaces;
using log4net;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace HistoryServiceAPI.Jobs
{
    public class AutomaticHistoryStartStopProvider
    {
        protected readonly IServiceProvider _serviceProvider;
        private readonly ILog _log;
        public string _gID { get; set; }

        public AutomaticHistoryStartStopProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }

        public int DelaySurveysStartStopTimeInterval(double secondsOnTopOfMidnight = 15)
        {
            double delayTime;
            delayTime = (DateTime.Today.AddDays(1) - DateTime.Now).TotalSeconds - secondsOnTopOfMidnight; // seconds to end of day
            //  ovde mozes re-podesiti broj sekundi do sledeceg pokretanja. Startovanje/zaustavljanje anketa ce raditi na trenutno vreme DateTime.Now
            //delayTime = 5; // 30; // sekunde
            _log.Info(_gID + "." + this.GetType().Name + "." + "DelaySurveysStartStopTimeInterval" + ": Delay time calculated in seconds - " + delayTime);

            return (int)delayTime;

        }

        public async Task StartSurveys(CancellationToken cancellationToken, double secondsOnTopOfMidnight = 15)
        {
            _gID = Guid.NewGuid().ToString();
            _log.Info(_gID + "." + this.GetType().Name + "." + "Service for Automatic Survey Start-Stop" + ": Start.");
            try
            {
                using (IServiceScope serviceScope = _serviceProvider.CreateScope())
                {
                    IHistoryService historyService =
                        serviceScope.ServiceProvider.GetRequiredService<IHistoryService>();

                       var result = await historyService.AutoStartHistoryAsync();
                     _log.Info(this.GetType().Name + "." + "Service for Automatic History Start-Stop" + ": Result. Number of started histories - " + result.StartedStoppedSurveyNumber + ", " + result?.AdditionalInfo);
                }
            }
            catch (Exception ex)
            {
                _log.Error(_gID + "." + this.GetType().Name + "." + "Service for Automatic History Start-Stop, Start" + ": Exception. " + ex.Message);
                Console.WriteLine(ex);
            }
            finally
            {
                _log.Info(_gID + "." + this.GetType().Name + "." + "Service for Automatic History Start-Stop" + ": Stop.");
            }
        }

        public async Task StoptSurveys(CancellationToken cancellationToken, double secondsOnTopOfMidnight = 15)
        {
            _gID = Guid.NewGuid().ToString();
            _log.Info(_gID + "." + this.GetType().Name + "." + "Service for Automatic History Start-Stop" + ": Start.");
            try
            {
                using (IServiceScope serviceScope = _serviceProvider.CreateScope())
                {
                    IHistoryService historyService =
                        serviceScope.ServiceProvider.GetRequiredService<IHistoryService>();

                    var result = await historyService.AutoStopHistoryAsync();
                    _log.Info(this.GetType().Name + "." + "Service for Automatic History Start-Stop" + ": Result. Number of stopped histories - " + result.StartedStoppedSurveyNumber + ", " + result?.AdditionalInfo);
                }
            }
            catch (Exception ex)
            {
                _log.Error(_gID + "." + this.GetType().Name + "." + "Service for Automatic History Start-Stop, Stop" + ": Exception. " + ex.Message);
                Console.WriteLine(ex);
            }
            finally
            {
                _log.Info(_gID + "." + this.GetType().Name + "." + "Service for auto assign questions to users" + ": Stop.");
            }
        }
    }
}
