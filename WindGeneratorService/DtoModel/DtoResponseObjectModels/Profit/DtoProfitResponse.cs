using System;
using System.Collections.Generic;
using System.Text;

namespace DtoModel.DtoResponseObjectModels.Profit
{
    public class DtoProfitResponse
    {
        public bool Success { get; set; }

        public double ProfitabillityIndex { get; set; }

        public double Profit { get; set; }

        public string Message { get; set; }

        public double CurrentWind { get; set; }
    }
}
