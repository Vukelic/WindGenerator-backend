using System;

namespace ConsoleAppWebJobPower
{
    class Program
    {
        static void Main(string[] args)
        {
            //OnDebug
            var powerService = new PowerService();
            powerService.OnDebug();
        }
    }
}
