using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.ResponseModels
{
    public class ReportDetail
    {
        public long Id { get; set; }
        public Guid UIID { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
        public long Report { get; set; }
        public int NearbyPersonCount { get; set; }
        public int NearbyPhoneNumberCount { get; set; }
    }
}
