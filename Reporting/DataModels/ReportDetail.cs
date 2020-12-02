using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataModels
{
    public class ReportDetail: DataModel
    {         
        public long Report { get; set; }
        public int NearbyPersonCount { get; set; }
        public int NearbyPhoneNumberCount { get; set; } 
    }
}
