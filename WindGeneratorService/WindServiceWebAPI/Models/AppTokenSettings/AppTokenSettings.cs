﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WindServiceWebAPI.Models.AppTokenSettings
{
    public class AppTokenSettings
    {
        public string Secret { get; set; }
        public int ExpiryTime { get; set; }
    }
}
