using System;

namespace ConsoleAppWebJobHistory
{
    class Program
    {
        static void Main(string[] args)
        {
            //OnDebug
            var historyService = new HistoryService();
            historyService.OnDebug();
        }
    }
}
