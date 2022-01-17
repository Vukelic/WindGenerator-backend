using Microsoft.Extensions.Configuration;
using System;
using System.ServiceProcess;
using WindService_WindowsService.Api;

namespace WindService_WindowsService
{
    public class Program
    {
        static void Main(string[] args)
        {

            //OnDebug
           // ApiHelper.InitializeClient();
            var totalSafetySrc = new WindService_Service();
            totalSafetySrc.OnDebug();

            //Default
          //  ServiceBase.Run(new WindService_Service());




           // var builder = new ConfigurationBuilder().AddJsonFile("", optional: true, reloadOnChange: true);



        }    


    }
}
