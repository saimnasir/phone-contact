﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reporting.ViewModels.Requests
{
    public class CreateReportRequest
    {
        public double Radius { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
