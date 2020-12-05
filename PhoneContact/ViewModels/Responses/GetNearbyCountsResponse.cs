using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneContact.ViewModels.Responses
{
    public class GetNearbyCountsResponse
    {
        public int NearbyPersonCount { get; set; }
        public int NearbyPhoneNumberCount { get; set; }
    }
}
